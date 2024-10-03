using Cursus_Data.Models.DTOs;
using Cursus_Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Common
{
    public class ExcelTable
    {
        public DataTable GetTable<T>(List<T> items, string tableName)
        {
            DataTable dt = new DataTable { TableName = tableName };

            var properties = typeof(T).GetProperties();
            foreach (var prop in properties)
            {
                dt.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            if (items.Count > 0)
            {
                foreach (var item in items)
                {
                    var row = dt.NewRow();
                    foreach (var prop in properties)
                    {
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    }
                    dt.Rows.Add(row);
                }
            }
            return dt;
        }
    }
}
