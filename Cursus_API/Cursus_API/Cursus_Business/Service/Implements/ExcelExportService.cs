using ClosedXML.Excel;
using Cursus_Business.Common;
using Cursus_Business.Service.Interfaces;
using Cursus_Data.Models.DTOs;
using Hangfire;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Implements
{
    public class ExcelExportService : IExcelExportService
    {
        private readonly IInstructorService _instructorService;
        private readonly ExcelTable _excelTable;
        public ExcelExportService(IInstructorService instructorService, ExcelTable excelTable)
        {
            _instructorService = instructorService;
            _excelTable = excelTable;
        }
        
        public async Task<Result> ExportToExcelAsync<T>(List<T> dataList, string tableName)
        {
            if (dataList == null)
            {
                return Result.Failure(Result.CreateError("EXCEPTION", "The list is null"));
            }

            var jobId = BackgroundJob.Enqueue(() => ExportExcelJob(dataList, tableName));
            return Result.SuccessWithObject(new
            {
                Message = "Export job enqueued successfully",
                JobId = jobId
            });
        }

        public async Task ExportExcelJob<T>(List<T> dataList, string tableName)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string excelDirectory = Path.Combine(currentDirectory, "Excel", tableName);
            if (!Directory.Exists(excelDirectory))
            {
                Directory.CreateDirectory(excelDirectory);
            }

            try
            {
                var dataTable = _excelTable.GetTable(dataList, tableName);
                var filePath = Path.Combine(excelDirectory, $"{tableName}_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add(dataTable);
                    workbook.SaveAs(filePath);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
