using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Models.DTOs
{
    public class ExceptionHandlerResponse
    {
        public required bool isSuccess { get; set; }
        public required bool isFailure { get; set; }
        public required ErrorResponse ErrorDetail { get; set; }
    }

    public class ErrorResponse
    {
        public required int code { get; set; }
        public required string message { get; set; }
    }
}
