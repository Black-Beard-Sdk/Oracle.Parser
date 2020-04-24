using Bb.ComponentModel.Accessors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exe.CompareModel
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
            var instanceProperties = AccessorItem.Get(instance.GetType(), true);

            for (int i = 0; i < args.Length; i++)
            {
                var propertyName = args[i];

                if (propertyName.StartsWith("-"))
                {

                    if (string.IsNullOrWhiteSpace(propertyName.Replace("-", "")))
                    {
                        throw new Exception(string.Format("argument at position {0} is invalid", i + 1));
                    }

                    propertyName = propertyName.Substring(1);
                    var value = (i + 1) <= args.Length ? args[i + 1] : string.Empty;
                    i++;

                    AccessorItem access = instanceProperties.FirstOrDefault(p => string.Compare(p.Name, propertyName, true) == 0);
                    if (access != null)
                        access.Unserialize(instance, value);
                    else
                    {
                        Console.WriteLine(string.Format("unresolved argument '{0}' at position {1}", propertyName, i + 1));
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

        /// <summary>
        /// Maps the properties between two objects.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        public static void MapProperties(object source, object destination)
        {
            var sourceProperties = AccessorItem.Get(source.GetType(), true);
            var destionationProperties = AccessorItem.Get(destination.GetType(), true);

            var commonproperties = from sp in sourceProperties
                                   join dp in destionationProperties on new { sp.Name, sp.Type } equals
                                       new { dp.Name, dp.Type }
                                   select new { sp, dp };

            foreach (var match in commonproperties)
                match.dp.SetValue(destination, match.sp.GetValue(source));

        }

        internal static string[] ToArray(object instance)
        {
            List<string> result = new List<string>();

            var instanceProperties = AccessorItem.Get(instance.GetType(), true);

            foreach (AccessorItem acc in instanceProperties)
            {
                string value = acc.Serialize(instance);
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
