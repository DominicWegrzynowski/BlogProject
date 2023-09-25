﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.Services
{
    public class BasicImageService : IImageService
    {
        public string ContentType(IFormFile file)
        {
            return file?.ContentType; // ? because the file might not be there
        }

        public string DecodeImage(byte[] data, string type)
        {
            if (data is null || type is null) return null;
            return $"data:image/{type};base64,{Convert.ToBase64String(data)}";
        }

        public async Task<byte[]> EncodeImageAsync(IFormFile file)
        {
            if (file is null) return null;

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);

            return ms.ToArray();
        }

        public async Task<byte[]> EncodeImageAsync(string fileName)
        {
            if (fileName is null) return null;
            var file = $"{ Directory.GetCurrentDirectory() }{ fileName }";

            return await File.ReadAllBytesAsync(file);
        }

        public int Size(IFormFile file)
        {
            return Convert.ToInt32(file?.Length);
        }
    }
}
