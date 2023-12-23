using AreaBox_V0._1.Areas.Admin.Models.CitiesModel.send;
using AreaBox_V0._1.Areas.Admin.Models.Countries.send;
using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Models.Dto;
using AutoMapper;

namespace AreaBox_V0._1.Common
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<MediaPosts, MediaPostsDto>()
				.ForMember(dest => dest.Id, src => src.MapFrom(src => src.MpostId))
				.ForMember(dest => dest.CategoryId, src => src.MapFrom(src => src.MpcategoryId))
				.ForMember(dest => dest.CityId, src => src.MapFrom(src => src.MpcityId)) // Add this line
				.ForMember(dest => dest.City, src => src.MapFrom(src => src.Mpcity))
				.ForMember(dest => dest.Date, src => src.MapFrom(src => src.Mpdate))
				.ForMember(dest => dest.UserId, src => src.MapFrom(src => src.MpuserId))
				.ForMember(dest => dest.State, src => src.MapFrom(src => src.Mpstate))
				.ForMember(dest => dest.Image, src => src.MapFrom(src => src.Mpimage))
				.ForMember(dest => dest.ShortDescription, src => src.MapFrom(src => src.MpshortDescription))
				.ForMember(dest => dest.LongDescription, src => src.MapFrom(src => src.MplongDescription))
				.ForMember(dest => dest.MediaPostLikes, opt => opt.MapFrom(src => src.MediaPostsLikes))
				.ForMember(dest => dest.PostLike, src => src.MapFrom(src => src.PostLike))
				.ForMember(dest => dest.MediaPostComments, opt => opt.MapFrom(src => src.MediaPostComments))
				.ForMember(dest => dest.Category, src => src.MapFrom(src => src.Mpcategory))
				.ForMember(dest => dest.User, src => src.MapFrom(src => src.Mpuser))
				;
			CreateMap<QuestionPosts, QuestionPostsDto>()
				.ForMember(dest => dest.Id, src => src.MapFrom(src => src.QpostId))
				.ForMember(dest => dest.CategoryId, src => src.MapFrom(src => src.QpcategoryId))
				.ForMember(dest => dest.CityId, src => src.MapFrom(src => src.QpcityId))
				.ForMember(dest => dest.Date, src => src.MapFrom(src => src.Qpdate))
				.ForMember(dest => dest.UserId, src => src.MapFrom(src => src.QpuserId))
				.ForMember(dest => dest.Title, src => src.MapFrom(src => src.Qptitle))
				.ForMember(dest => dest.Description, src => src.MapFrom(src => src.Qpdescription))
				.ForMember(dest => dest.State, src => src.MapFrom(src => src.Qpstate))
				.ForMember(dest => dest.Category, src => src.MapFrom(src => src.Qpcategory))
				.ForMember(dest => dest.City, src => src.MapFrom(src => src.Qpcity))
				.ForMember(dest => dest.User, src => src.MapFrom(src => src.Qpuser))
				.ForMember(dest => dest.QuestionPostComments, opt => opt.MapFrom(src => src.QuestionPostComments))
				;

			CreateMap<MediaPostsReports, MediaPostsReportsDto>()
				;

			CreateMap<QuestionPostsReports, QuestionPostsReportsDto>()
				;
			CreateMap<ApplicationUser, ApplicationUserDto>()
				;

			CreateMap<Countries, CountriesDto>()
				;
			CreateMap<Cities, CitiesDto>()
				;
			CreateMap<Categories, CategoriesDto>()
				;

			CreateMap<Countries, CountriesDtoForApi>();
			CreateMap<Cities, CitiesDtoForApi>();
			CreateMap<PostReports, PostReportsDto>();
		}
	}
}
