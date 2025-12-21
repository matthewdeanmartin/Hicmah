// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;

namespace Ukadc.Diagnostics.Utils
{
    /// <summary>
    /// Used to dynamically read the value from a property whose name and type are known only at run-time. The constructor uses
    /// Lightweight Code Generation (LCG) to create a <see cref="DynamicMethod"/> which reads the property value.
    /// </summary>
    /// <remarks>
    /// <para>
    /// When writing log data we took the option of allowing the user to include extra information, and to use this in various
    /// places whilst logging. As an example you might want to be able to define the logging filename based on criteria such
    /// as the current day name.
    /// </para>
    /// <para>
    /// In order to support this style of property access we decided upon an extensible tokenisation system which would allow you
    /// to write something such as "{Day}-{Month}-{Year}" and so on inside the configuration file. Here the items within curly
    /// braces are called tokens.
    /// </para>
    /// <para>
    /// As another example of where this could be used, we've created the 
    /// <see cref="Ukadc.Diagnostics.Listeners.OutputDebugStringTraceListener">OutputDebugStringTraceListener</see>
    /// which writes its output to the standard debug stream, however it writes this output in a formatted manner which under the covers
    /// uses the FastPropertyGetter. In this case the format specification is defined as the <bold>initializeData</bold> attribute
    /// within the configiration file.
    /// </para>
    /// <para>
    /// So, when we receive an event we need a (rapid!) way to format this data, and part of the solution was the FastPropertyGetter
    /// class. As an example, say we wanted to permit you to use the <bold>Name</bold> property of the <bold>Process</bold> class as a
    /// token. But then you might want to use the <bold>Handle</bold> property, or maybe the <bold>MainModule.FileName</bold> property.
    /// We needed some way to allow tokens to be defined, but then permit you to get individual properties at runtime - where these
    /// properties could be at an arbitrary depth (i.e. some object.propertyA.propertyB.propertyC). Enter the FastPropertyGetter.
    /// </para>
    /// <para>
    /// We decided upon a simple mechanism for defining tokens and properties. To use the earlier Process example, we simply defined a
    /// 'Process' token, and then permit the user to add on property (and field) names to that root token. In your configuration file
    /// you might defined 'Process.MainModule.FileVersionInfo.ProductVersion'. When we parse this (this parsing happens just once) we
    /// identify the token type ('Process') and also the property path ('MainModule.FileVersionInfo.ProductVersion'). We can then 
    /// build an object that will read this data at runtime, given an instance of the process class.
    /// </para>
    /// <para>
    /// We chose an implementation that would use code generation as we wanted this to be as performant as possible, and using
    /// Reflection at runtime to traverse property values would simply not have been rapid enough. So, when we need to parse some
    /// arbitrary property path we use this class which is passed the property path and object type to its constructor. The magic
    /// happens internally as we then generate a delegate using <a href="http://msdn2.microsoft.com/system.reflection.emit">Reflection.Emit</a> 
    /// which we call to retrieve the runtime value from the process instance.
    /// </para>
    /// <para>
    /// The code generated is obviously based on the object passed in and the requested property path - we generate some checks
    /// in order to ensure that the appropriate data has been passed into the function, and then traverse the path of properties
    /// and fields in order to retrieve the data requested.
    /// </para>
    /// <para>
    /// One last thing - the generation of the FastPropertyGetter is always strongly typed so if class Foo has a property called Bar
    /// of type object, it would not be valid to try to retrieve Foo.Bar.Banana, even if the object in Bar at runtime had a Banana
    /// property or field.
    /// </para>
    /// </remarks>
    public class FastPropertyGetter
    {
        /// <summary>
        /// Delegate that is used internally to invoke the property getter
        /// </summary>
        /// <param name="data">The object on from which to retrieve the property</param>
        /// <returns>A property result indicating that the property was read and also the value of the property</returns>
        private delegate PropertyResult GetData(object data);

        /// <summary>
        /// Stores the delegate for use when the user calls the GetValue method
        /// </summary>
        private GetData GDDelegate;

        /// <summary>
        /// Creates a <see cref="FastPropertyGetter"/> that reads the property with the name "propertyName"
        /// from the Type specified in "type"
        /// </summary>
        /// <param name="propertyName">The name of the property to be read</param>
        /// <param name="type">The name of the type to which the property belongs</param>
        public FastPropertyGetter(string propertyName, Type type)
        {
            Type t;
            GDDelegate = CreateGetValueDelegate(type, propertyName, out t);

            PropertyType = t;
        }

        /// <summary>
        /// Gets the Type the property returns
        /// </summary>
        public Type PropertyType
        {
            get;
            private set;
        }

        /// <summary>
        /// Return a delegate which, when invoked, will return the value of the named property/field of the passed type
        /// </summary>
        /// <param name="t">The root type</param>
        /// <param name="properties">A dotted list of property names</param>
        /// <param name="returnType">The type returned from the named property/field</param>
        /// <returns>A delegate which, when invoked with an instance of 't' will return the value of the property/field requested</returns>
        private static GetData CreateGetValueDelegate(Type t, string properties, out Type returnType)
        {
            if (null == t)
                throw new ArgumentNullException("t");
            if (string.IsNullOrEmpty(properties))
                throw new ArgumentNullException("properties");

            string[] props = properties.Split('.');

            DynamicMethod dm = new DynamicMethod("TheMethodName", typeof(PropertyResult), new Type[] { typeof(object) }, typeof(FastPropertyGetter));
            ILGenerator il = dm.GetILGenerator();

            // Emit the code that will define the return value
            LocalBuilder returnedPropertyResult = il.DeclareLocal(typeof(PropertyResult));

            il.Emit(OpCodes.Newobj, typeof(PropertyResult).GetConstructor(new Type[] { }));
            il.Emit(OpCodes.Stloc, returnedPropertyResult.LocalIndex);

            // Local used when traversing properties
            LocalBuilder temp = il.DeclareLocal(typeof(object));

            // Define the label we jump to if there's an error of some kind in the function
            Label nullReturn = il.DefineLabel();

            // Emit the code to test that the input argument is non-null
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Brfalse, nullReturn);

            // Now test that the argument is the correct type
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Isinst, t);
            il.Emit(OpCodes.Brfalse, nullReturn);

            // Load the candidate object onto the stack
            il.Emit(OpCodes.Ldarg_0);

            if (t.IsValueType)
            {
                // Unbox the object if it's a value type
                LocalBuilder lb = il.DeclareLocal(t);
                il.Emit(OpCodes.Unbox_Any, t);
                il.Emit(OpCodes.Stloc, lb.LocalIndex);
                il.Emit(OpCodes.Ldloca_S, lb.LocalIndex);
            }

            // Now emit calls to get the appropriate property from the object
            Type candidateType = t;

            // Construct the code which will walk down through the properties/fields
            for (int pos = 0; pos < props.Length; pos++)
            {
                string elementName = props[pos];

                // Find a property called propertyName on the candidate type
                PropertyInfo pi = candidateType.GetProperty(elementName);
                Type propType = null;

                if (null == pi)
                {
                    // No property with that name - try a field
                    FieldInfo fi = candidateType.GetField(elementName);

                    // No field either - splat
                    if (null == fi)
                        throw new ArgumentException(string.Format(Resources.NoPropOrFieldOrInaccessible, elementName, candidateType), "t");

                    // Record the type of the field for later and then load the value of the field
                    propType = fi.FieldType;
                    il.Emit(OpCodes.Ldfld, fi);
                }
                else
                {
                    // It's a property but can I read it?
                    if (!pi.CanRead)
                        throw new ArgumentException(string.Format(Resources.NoPropOrFieldOrInaccessible, elementName, candidateType), "t");

                    // Get the get method
                    MethodInfo mi = pi.GetGetMethod();

                    // And emit a call to get the property - the return value will be on the stack and so is used for the next call (if there is one)
                    if (mi.IsFinal)
                        il.Emit(OpCodes.Call, mi);
                    else
                        il.Emit(OpCodes.Callvirt, mi);

                    propType = pi.PropertyType;
                }

                // I now have the property/field value on the stack and need to do something with it
                if (propType.IsValueType)
                {
                    // If this is anything but the last property/field I need to load up the address of the data on the stack
                    if (pos < (props.Length - 1))
                    {
                        // Store in a local and then load the address of the object
                        // but only if this is not the last property/field - otherwise you'll see
                        // the address of the object as the return value, which won't be much use!
                        LocalBuilder lb = il.DeclareLocal(propType);
                        il.Emit(OpCodes.Stloc, lb.LocalIndex);
                        il.Emit(OpCodes.Ldloca, lb.LocalIndex);
                    }
                }
                else
                {
                    // Verify that the object is an instance of the appropriate type
                    il.Emit(OpCodes.Stloc, temp.LocalIndex);
                    il.Emit(OpCodes.Ldloc, temp.LocalIndex);
                    il.Emit(OpCodes.Isinst, propType);

                    // If no jump out
                    il.Emit(OpCodes.Brfalse, nullReturn);
                    // If yes, load the object onto the stack again and continue
                    il.Emit(OpCodes.Ldloc, temp.LocalIndex);
                }

                // Move up to the next type
                candidateType = propType;
            }

            // Box the return value if necessary
            if (candidateType.IsValueType)
                il.Emit(OpCodes.Box, candidateType);

            // Now store the property/field value locally in the temp variable
            il.Emit(OpCodes.Stloc, temp.LocalIndex);

            il.Emit(OpCodes.Ldloc, returnedPropertyResult.LocalIndex);

            // Set the 'Data' property to whatever was retrieved in this method
            il.Emit(OpCodes.Ldloc, temp.LocalIndex);
            il.Emit(OpCodes.Callvirt, typeof(PropertyResult).GetProperty("Data").GetSetMethod());

            // Earlier code jumps here to return (bypassing the call above to set the Data property of the return value)
            il.MarkLabel(nullReturn);

            // Load up the return value
            il.Emit(OpCodes.Ldloc, returnedPropertyResult.LocalIndex);
            il.Emit(OpCodes.Ret);

            // Now return the final return type and the delegate to invoke
            returnType = candidateType;

            return dm.CreateDelegate(typeof(GetData)) as GetData;
        }

        /// <summary>
        /// Attempts to read the property from the specified object
        /// </summary>
        /// <param name="data">The object from which </param>
        /// <returns>A <see cref="PropertyResult" /></returns>
        public PropertyResult GetValue(object data)
        {
            return GDDelegate(data);
        }
    }
}