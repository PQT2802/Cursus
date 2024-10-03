using Cursus_Business.Service.Interfaces;
using Cursus_Data.Models.Entities;
using Cursus_Data.Repositories.Implements;
using Cursus_Data.Repositories.Interfaces;
using FirebaseAdmin.Auth;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Business.Service.Implements
{
    public class FirebaseService : IFirebaseService
    {
        private readonly StorageClient _storageClient;
        private readonly IUserRepository _userRepository;
        private readonly string _bucketName = "lmsproject-5a473.appspot.com";
        public FirebaseService(StorageClient storageClient, IUserRepository userRepository)
        {
            _storageClient = storageClient;
            _userRepository = userRepository;
        }

        public async Task<MemoryStream> GetImage(string filePath)
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream();

                await _storageClient.DownloadObjectAsync(_bucketName, filePath, memoryStream);

                memoryStream.Seek(0, SeekOrigin.Begin);

                return memoryStream;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<string> UploadImage(IFormFile file, string folder)
        {
            if (file is null || file.Length == 0)
            {
                return  "File is empty!";
            }

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";

            var filePath = $"{folder}/{fileName}";

            string url;

            await using (var stream = file.OpenReadStream())
            {
                var result = await _storageClient.UploadObjectAsync(_bucketName, filePath, null, stream);
            }

            return filePath.ToString();
        }

    }
}
