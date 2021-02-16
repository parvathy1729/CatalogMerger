using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CatalogMerger.Util
{
    public static class WriteCsv
    {
        private static string ToCsvHeader(this object obj)
        {
            Type type = obj.GetType();
            var properties = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

            string result = string.Empty;
            Array.ForEach(properties, prop =>
            {
                var propertyType = prop.PropertyType.FullName;
                if (propertyType == "System.String")
                {
                    result += prop.Name + ",";
                }
            });

            return (!string.IsNullOrEmpty(result) ? result.Substring(0, result.Length - 1) : result);
        }

        private static string ToCsvRow(this object obj)
        {
            Type type = obj.GetType();
            var properties = type.GetProperties(BindingFlags.DeclaredOnly |
                                                BindingFlags.Public |
                                                BindingFlags.Instance);

            string result = string.Empty;
            Array.ForEach(properties, prop =>
            {
                var value = prop.GetValue(obj, null);
                var propertyType = prop.PropertyType.FullName;
                if (propertyType == "System.String")
                {
                    value = "\"" + value + "\"";
                }

                result += value + ",";

            });

            return (!string.IsNullOrEmpty(result) ? result.Substring(0, result.Length - 1) : result);
        }

        public static bool SaveCsvFile(this object obj, string outputDirectory, string fileName)
        {
            var collection = ((IEnumerable)obj).Cast<object>().ToList();
            var output = collection.First().ToCsvHeader();
            output += Environment.NewLine;

            collection.ToList().ForEach(rows =>
            {
                output += rows.ToCsvRow();
                output += Environment.NewLine;
            });
            var path = outputDirectory + @"\" + fileName;
            if (File.Exists(path))
                File.Delete(path);
            File.WriteAllText(path, output);
            return File.Exists(path);
        }

    }
}
