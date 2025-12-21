// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System;
using System.Configuration;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;

namespace Ukadc.Diagnostics.Utils
{
    /// <summary>
    /// This class is used by the Ukadc.Diagnostics library to generate runtime objects such as 
    /// TraceFilters using their initializeData strings. It is _almost_ a carbon copy of the 
    /// internal (Grr!) TraceUtils class inside the System.Diagnostics namespace with a few 
    /// unnecessary pieces removed.
    /// </summary>
    public static class TraceUtils
    {
        /// <summary>
        /// Creates a runtime object as specified by the className
        /// </summary>
        /// <param name="className">The class to construct</param>
        /// <param name="baseType">The type from which the class MUST inherit</param>
        /// <param name="initializeData">The data used to construct the instance (or null for default constructor)</param>
        /// <returns>An instance of the class specified by className parameter</returns>
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes"),
         SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public static object GetRuntimeObject(string className, Type baseType, string initializeData)
        {
            if (string.IsNullOrEmpty(className))
                throw new ArgumentNullException("className");
            if (null == baseType)
                throw new ArgumentNullException("baseType");

            object runtimeObject = null;

            // Lookup the type - it may be a fully qualified name
            Type classType = Type.GetType(className, false);

            try
            {
                // If not found, see if it's just a shortened version from System.Diagnostics
                if (null == classType)
                {
                    classType = typeof (TraceFilter).Assembly.GetType(className);
                    if (null == classType)
                        throw new TypeLoadException(className);
                }

                // Now I have the type, see if it's assignable from the classType
                if (!baseType.IsAssignableFrom(classType))
                    throw new ArgumentException(
                        string.Format(CultureInfo.CurrentCulture, Resources.NotAssignableFrom, className, baseType));

                // If I have no initial data, look for an empty ctor
                if (string.IsNullOrEmpty(initializeData))
                {
                    ConstructorInfo constructor = classType.GetConstructor(Type.EmptyTypes);
                    if (constructor == null)
                        throw new ConfigurationErrorsException(
                            string.Format(CultureInfo.CurrentCulture, Resources.CouldNotFindEmptyConstructor, className));

                    runtimeObject = constructor.Invoke(new object[0]);
                }
                else
                {
                    // Look for a string based ctor
                    ConstructorInfo constructor = classType.GetConstructor(new Type[] {typeof (string)});
                    if (constructor != null)
                        runtimeObject = constructor.Invoke(new object[] {initializeData});
                    else
                    {
                        // Now I have to look for all one parameter constructors
                        ConstructorInfo[] constructors = classType.GetConstructors();

                        for (int i = 0; i < constructors.Length; i++)
                        {
                            ParameterInfo[] parameters = constructors[i].GetParameters();
                            if (parameters.Length == 1)
                            {
                                // I have one - try converting to the correct type
                                // This will lookup an enum value (if it's an enum), or if not
                                // try the last-didth Convert.ChangeType method
                                Type parameterType = parameters[0].ParameterType;
                                try
                                {
                                    object parameter = ConvertToBaseTypeOrEnum(initializeData, parameterType);
                                    runtimeObject = constructors[i].Invoke(new object[] {parameter});
                                    break;
                                }
                                catch (Exception)
                                {
                                    // Ignore this - try the next single parameter ctor
                                }
                            }
                        }

                        // If I've not created one yet, I couldn't find an appropriate ctor so throw.
                        if (null == runtimeObject)
                            throw new Exception(
                                string.Format(CultureInfo.CurrentCulture, Resources.NoConstructorFound, className,
                                              initializeData));
                    }
                }

                return runtimeObject;
            }
            catch (Exception ex)
            {
                throw new ConfigurationErrorsException(
                    string.Format(CultureInfo.CurrentCulture, Resources.CouldNotCreateInstanceOfType, className), ex);
            }
        }

        /// <summary>
        /// Tries to convert a string value to an enum or other specified type
        /// </summary>
        /// <param name="value">The value to convert</param>
        /// <param name="type">The target type</param>
        /// <returns>The converted value</returns>
        public static object ConvertToBaseTypeOrEnum(string value, Type type)
        {
            if (type.IsEnum)
                return Enum.Parse(type, value, false);

            return Convert.ChangeType(value, type, CultureInfo.CurrentCulture);
        }
    }
}