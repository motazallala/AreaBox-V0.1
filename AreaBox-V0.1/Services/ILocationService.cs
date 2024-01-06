using Microsoft.AspNetCore.Mvc;

namespace AreaBox_V0._1.Services
{
	public interface ILocationService
	{
		Task<string> GetGeolocation(double latitude, double longitude);
	}
}
