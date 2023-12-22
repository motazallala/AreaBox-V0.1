using AreaBox_V0._1.Areas.Admin.Models.CitiesModel.send;

namespace AreaBox_V0._1.Areas.Admin.Models.Countries.send;

public class CountriesDtoForApi
{
    public int CountryId { get; set; }

    public string CountryName { get; set; }
    public virtual ICollection<CitiesDtoForApi> Cities { get; set; }
}
