using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Tiff;
using SixLabors.ImageSharp.Processing;

namespace AreaBox_V0._1.Services
{
    public class ImageService : IImageService
    {
        public async Task<string> UploadImage(IFormFile file)
        {
            var format = DetermineImageFormat(file.ContentType);
            using var image = await Image.LoadAsync(file.OpenReadStream());

            double percentage = PercentageBasedOnSize(file.Length);

            if (file.Length > 0.25 * 1024 * 1024)
            {
                int newWidth = (int)(image.Width * percentage);
                int newHeight = (int)(image.Height * percentage);
                image.Mutate(x => x.Resize(newWidth, newHeight));
            }

            return GenerateBase64DataUri(image, format);
        }

        public async Task<string> UploadLocalImage(string filePath)
        {
            var format = DetermineImageFormatFromFilePath(filePath);
            using var image = await Image.LoadAsync(filePath);

            double percentage = PercentageBasedOnSize(new FileInfo(filePath).Length);

            if (new FileInfo(filePath).Length > 0.25 * 1024 * 1024)
            {
                int newWidth = (int)(image.Width * percentage);
                int newHeight = (int)(image.Height * percentage);
                image.Mutate(x => x.Resize(newWidth, newHeight));
            }

            return GenerateBase64DataUri(image, format);
        }

        private IImageEncoder DetermineImageFormat(string contentType)
        {
            return contentType switch
            {
                "image/jpeg" or "image/jpg" => new JpegEncoder(),
                "image/png" => new PngEncoder(),
                "image/gif" => new GifEncoder(),
                "image/bmp" => new BmpEncoder(),
                "image/tiff" => new TiffEncoder(),
                _ => new PngEncoder(),
            };
        }

        private IImageEncoder DetermineImageFormatFromFilePath(string filePath)
        {
            var extension = Path.GetExtension(filePath)?.TrimStart('.').ToLowerInvariant();

            return extension switch
            {
                "jpeg" or "jpg" => new JpegEncoder(),
                "png" => new PngEncoder(),
                "gif" => new GifEncoder(),
                "bmp" => new BmpEncoder(),
                "tiff" => new TiffEncoder(),
                _ => new PngEncoder(),
            };
        }

        private double PercentageBasedOnSize(long fileSize)
        {
            return fileSize switch
            {
                long size when size > 25 * 1024 * 1024 => 0.05,
                long size when size > 10 * 1024 * 1024 => 0.10,
                long size when size > 5 * 1024 * 1024 => 0.15,
                long size when size > 1 * 1024 * 1024 => 0.20,
                long size when size > 0.5 * 1024 * 1024 => 0.50,
                long size when size > 0.25 * 1024 * 1024 => 0.80,
                _ => 1.0
            };
        }

        private string GenerateBase64DataUri(Image image, IImageEncoder encoder)
        {
            using var outputStream = new MemoryStream();
            image.Save(outputStream, encoder);

            var mimeType = encoder switch
            {
                JpegEncoder _ => "image/jpeg",
                PngEncoder _ => "image/png",
                GifEncoder _ => "image/gif",
                BmpEncoder _ => "image/bmp",
                TiffEncoder _ => "image/tiff",
                _ => "image/png"
            };

            return $"data:{mimeType};base64,{Convert.ToBase64String(outputStream.ToArray())}";
        }
    }
}
