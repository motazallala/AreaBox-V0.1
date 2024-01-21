using AreaBox_V0._1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NuGet.Configuration;
using System.Net.Http;

namespace AreaBox_V0._1.Areas.User.Controllers;
[Area("User")]
[Route("[controller]/[action]")]
public class ChatController : Controller
{
	private readonly ILocationService _locationService;

	public ChatController(ILocationService locationService)
	{
		_locationService = locationService;
	}

	public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetGeolocation(double latitude, double longitude)
    {
		var response = await _locationService.GetGeolocation(latitude, longitude);

		if(response == null)
		{
			return BadRequest("Configuration values not found.");
		}

		return Ok(response);
		
	}

}
