using AreaBox_V0._1.Models.Location;

namespace AreaBox_V0._1.Services
{
    public interface ILocationService
    {
        Task<string> GetGeolocation(double latitude, double longitude);
        Task<GeolocationInfo> GetGeolocationObject(double latitude, double longitude);

    }
}
