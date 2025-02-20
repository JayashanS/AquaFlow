using AquaFlow.Domain.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;


namespace FishFarm.Infrastructure.Services
{
   

    public class ImageProcessingService : IImageProcessingService
    {
        private readonly int _jpegQuality;

        public ImageProcessingService(IConfiguration configuration)
        {
            _jpegQuality = int.Parse(configuration["ImageProcessing:JpegQuality"] ?? "80");

        }

        public async Task<byte[]> ConvertToJpgAsync(IFormFile file, int maxWidth = 1920, int maxHeight = 1080)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("No file provided");

            if (!IsImageValid(file))
                throw new ArgumentException("Invalid image file");

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            using var image = await Image.LoadAsync(memoryStream);

            if (image.Width > maxWidth || image.Height > maxHeight)
            {
                var options = new ResizeOptions
                {
                    Mode = ResizeMode.Max,
                    Size = new Size(maxWidth, maxHeight)
                };
                image.Mutate(x => x.Resize(options));
            }

            // Configure JPEG encoding
            var jpegEncoder = new JpegEncoder
            {
                Quality = _jpegQuality,
                //ColorType = JpegColorType.YCbCrRatio420
            };

            using var outputStream = new MemoryStream();
            await image.SaveAsJpegAsync(outputStream, jpegEncoder);
            return outputStream.ToArray();
        }

        public bool IsImageValid(IFormFile file)
        {
            if (file == null)
                return false;

            // Check file extension
            var validExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!validExtensions.Contains(extension))
                return false;

            try
            {
                // Attempt to load the image to verify it's valid
                using var stream = file.OpenReadStream();
                using var image = Image.Load(stream);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}