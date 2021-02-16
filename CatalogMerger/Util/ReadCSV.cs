using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace CatalogMerger.Util
{
    public static class ReadCsv
    {
        public static DataTable ConvertCsvToDataTable(string strFilePath)
        {
            DataTable dt = new DataTable();

            if (string.IsNullOrWhiteSpace(strFilePath))
                return dt;

            using (var sr = new StreamReader(strFilePath))
            {
                var headers = sr.ReadLine()?.Split(',');

                if (headers == null)
                    return dt;

                foreach (var header in headers)
                {
                    dt.Columns.Add(header);
                }

                while (!sr.EndOfStream)
                {
                    var rows = sr.ReadLine()?.Split(',');
                    DataRow dr = dt.NewRow();
                    for (var i = 0; i < headers.Length; i++)
                    {
                        if (rows != null)
                            dr[i] = rows[i].Trim('"');
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        public static List<T> ConvertToList<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name.ToLower()))
                    {
                        pro.SetValue(objT, row[pro.Name]);
                    }
                }
                return objT;
            }).ToList();
        }

    }
}
