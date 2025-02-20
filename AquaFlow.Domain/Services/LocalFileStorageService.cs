using AquaFlow.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace AquaFlow.Domain.Services
{
    public class LocalFileStorageService : ILocalFileStorageService
    {
        private readonly string _baseStoragePath;
        private readonly string _baseUrlPath;

        public LocalFileStorageService(IConfiguration configuration)
        {
            _baseStoragePath = configuration["FileStorage:BasePath"] ?? "wwwroot/uploads";
            _baseUrlPath = configuration["FileStorage:BaseUrl"] ?? "/uploads";
        }

        public async Task<string> SaveImageAsync(IFormFile file, string folder)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("No file provided");

            if (!IsImageValid(file))
                throw new ArgumentException("Invalid image file");

            var folderPath = Path.Combine(_baseStoragePath, folder);
            Directory.CreateDirectory(folderPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Path.Combine(_baseUrlPath, folder, fileName).Replace("\\", "/");
        }

        public async Task DeleteImageAsync(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return;

            var physicalPath = Path.Combine(_baseStoragePath,
                filePath.Replace(_baseUrlPath, "").TrimStart('/'));

            if (File.Exists(physicalPath))
            {
                await Task.Run(() => File.Delete(physicalPath));
            }
        }

        public bool IsImageValid(IFormFile file)
        {
            if (file == null)
                return false;

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(extension))
                return false;

            var signatures = new List<byte[]>
            {
                new byte[] { 0xFF, 0xD8 },              // JPEG
                new byte[] { 0x89, 0x50, 0x4E, 0x47 },  // PNG
                new byte[] { 0x47, 0x49, 0x46 }         // GIF
            };

            using (var reader = new BinaryReader(file.OpenReadStream()))
            {
                var headerBytes = reader.ReadBytes(4);
                return signatures.Any(signature =>
                    headerBytes.Take(signature.Length).SequenceEqual(signature));
            }
        }
    }
}