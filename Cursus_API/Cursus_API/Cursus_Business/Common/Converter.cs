using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Common
{
    public static class Converter
    {
        public static IFormFile ConvertBase64ToIFormFile(string base64String, string fileName)
        {
            if (string.IsNullOrEmpty(base64String))
            {
                throw new ArgumentException("Base64 string cannot be null or empty", nameof(base64String));
            }

            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("File name cannot be null or empty", nameof(fileName));
            }

            // Handle cases where the Base64 string has a prefix
            var base64Data = base64String;
            if (base64Data.Contains(","))
            {
                base64Data = base64Data.Substring(base64Data.IndexOf(',') + 1);
            }

            // Convert Base64 string to byte array
            byte[] fileBytes;
            try
            {
                fileBytes = Convert.FromBase64String(base64Data);
            }
            catch (FormatException ex)
            {
                throw new ArgumentException("Invalid Base64 string", nameof(base64String), ex);
            }

            // Create a MemoryStream from the byte array
            var stream = new MemoryStream(fileBytes);

            // Create a FormFile from the MemoryStream without setting ContentType
            var formFile = new FormFile(stream, 0, fileBytes.Length, "file", fileName);

            return formFile;
        }
    }
}
