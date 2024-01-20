namespace AreaBox_V0._1.Services
{
    public interface IImageService
    {
        Task<string> UploadImage(IFormFile file);
        Task<string> UploadLocalImage(string filePath);
    }
}
