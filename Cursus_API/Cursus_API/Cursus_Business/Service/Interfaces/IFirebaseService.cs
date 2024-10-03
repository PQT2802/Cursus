using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Interfaces
{
    public interface IFirebaseService
    {
        Task<String> UploadImage(IFormFile file, string folder);
        Task<MemoryStream> GetImage(string filePath);
    }
}
