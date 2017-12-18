using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Bb.Oracle.Helpers
{

    /// <summary>
    /// Argument helper
    /// </summary>
    public static class ArgumentHelper
    {

        /// <summary>
        /// Maps the arguments.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="args">The arguments.</param>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static string[] MapArguments<T>(string[] args, T instance)
        {
            List<string> unresolvedArgs = new List<string>();
            var instanceProperties = instance.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public).ToDictionary(c => c.Name.ToLower());

            for (int i = 0; i < args.Length; i++)
            {
                var propertyName = args[i];

                if (propertyName.StartsWith("-"))
                {
                    propertyName = propertyName.Substring(1);
                    var value = (i + 1) <= args.Length ? args[i + 1] : string.Empty;
                    i++;

                    PropertyInfo access = instanceProperties[propertyName.ToLower()];
                    if (access != null)
                        access.SetValue(instance, Convert.ChangeType(value, access.PropertyType));
                    else
                    {
                        unresolvedArgs.Add(propertyName);
                        unresolvedArgs.Add(value);
                    }
                }
                else
                    unresolvedArgs.Add(propertyName);
            }

            // TODO: Complete member initialization
            return unresolvedArgs.ToArray();
        }

        public static string[] ToArray(object instance)
        {

            List<string> result = new List<string>();

            var type = instance.GetType();

            var instanceProperties = instance.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (PropertyInfo acc in instanceProperties)
            {
                string value = (string)Convert.ChangeType(acc.GetValue(instance), typeof(string));
                if (!string.IsNullOrEmpty(value))
                {
                    result.Add("-" + acc.Name);
                    result.Add(@"""" + value + @"""");
                }
            }

            return result.ToArray();

        }
    }


}
