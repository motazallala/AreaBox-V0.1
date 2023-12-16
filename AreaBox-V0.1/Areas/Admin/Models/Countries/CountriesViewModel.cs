using AreaBox_V0._1.Areas.Admin.Models.CitiesModel;

namespace AreaBox_V0._1.Areas.Admin.Models.Countries;

public class CountriesViewModel
{
	public int CountryId { get; set; }

	public string CountryName { get; set; }
	public virtual ICollection<CitiesViewModel> Cities { get; set; }
}
