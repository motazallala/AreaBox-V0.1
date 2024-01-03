
using System.Drawing.Imaging;
using System.Drawing;

namespace AreaBox_V0._1.Services
{
    public class ImageService : IImageService
    {
        public async Task<string> UploadImage(IFormFile file)
        {

			if (file.Length > 10 * 1024 * 1024)
            {
                using var image = Image.FromStream(file.OpenReadStream());
                var newWidth = (int)(image.Width * 0.5);
                var newHeight = (int)(image.Height * 0.5);

                using var resizedImage = new Bitmap(newWidth, newHeight);
                using var graphics = Graphics.FromImage(resizedImage);

                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

                var outputStream = new MemoryStream();
                resizedImage.Save(outputStream, ImageFormat.Jpeg);
                outputStream.Position = 0;
                file = new FormFile(outputStream, 0, outputStream.Length, "image", file.FileName);
            }

            return await ConvertToBase64(file);
        }

        private async Task<string> ConvertToBase64(IFormFile file)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                byte[] bytes = memoryStream.ToArray();
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
