using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using WebApplicationTask.Application.Common.Exceptions;
using WebApplicationTask.Application.Common.Helpers;
using WebApplicationTask.Application.Services;

namespace WebApplicationTask.Application.Services
{
    public class FileService(IWebHostEnvironment webHostEnvironment) : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

        public void DeleteImage(string fileName)
        {
            var wwwrootFolder = _webHostEnvironment.WebRootPath;
            var filePath = Path.Combine(wwwrootFolder, fileName.TrimStart('~', '/'));

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public string UploadImage(IFormFile file)
        {
            if (FileSizeHelper.ByteToMb(file.Length) > 5)
            {
                throw new StatusCodeException(System.Net.HttpStatusCode.BadRequest, "Image size exceeds the limit of 5MB.");
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(file.FileName)?.ToLower();
            if (!allowedExtensions.Contains(extension))
            {
                throw new StatusCodeException(System.Net.HttpStatusCode.BadRequest, "Invalid image file format. Only PNG and JPEG are allowed.");
            }

            var wwwrootFolder = _webHostEnvironment.WebRootPath;
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(wwwrootFolder, "images", uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            Console.WriteLine($"Image uploaded to: {filePath}");

            return $"/images/{uniqueFileName}"; 
        }

    }
}