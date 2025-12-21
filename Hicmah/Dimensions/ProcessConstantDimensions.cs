using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hicmah.Dimensions
{
    /// <summary>
    /// This copies enums to dimension tables.
    /// </summary>
    /// <remarks>
    /// These are unexpected to change, except on version upgrades.
    /// </remarks>
    public class ProcessConstantDimensions
    {
        public void Process()
        {
            foreach (RequestType value in Enum.GetValues(typeof(RequestType)))
            {
                using(InsertEnum insertEnum = new InsertEnum("request_types"))
                {
                    try
                    {
                        insertEnum.Insert(Convert.ToInt32(value), value.ToString(), GetDescription(value, value.ToString()));
                        insertEnum.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Message.Contains("Violation of PRIMARY KEY constraint"))
                            continue;
                    }
                    
                }
            }

            foreach (Invoker value in Enum.GetValues(typeof(Invoker)))
            {
                using (InsertEnum insertEnum = new InsertEnum("invoker"))
                {
                    try
                    {
                        insertEnum.Insert(Convert.ToInt32(value), value.ToString(), GetDescription(value, value.ToString()));
                        insertEnum.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Message.Contains("Violation of PRIMARY KEY constraint"))
                            continue;
                    }
                }
            }
            
        }

        public static string GetDescription(Enum value, string defaultValue)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            if (fi == null)
                return defaultValue;

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                                                typeof(DescriptionAttribute), false);
            if (attributes[0] == null)
                return defaultValue;

            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }
    }
}
