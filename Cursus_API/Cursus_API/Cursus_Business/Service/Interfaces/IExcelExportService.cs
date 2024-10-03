using Cursus_Business.Common;
using Cursus_Data.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Interfaces
{
    public interface IExcelExportService
    {
        Task<Result> ExportToExcelAsync<T>(List<T> dataList, string tableName);
    }
}
