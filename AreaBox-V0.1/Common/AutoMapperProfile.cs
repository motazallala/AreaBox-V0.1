using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Models.MediaPost;
using AreaBox_V0._1.Models.QuestionPost;
using AutoMapper;
using System.Composition;

namespace AreaBox_V0._1.Common
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<MediaPosts, MediaPostViewModel>();
            CreateMap<QuestionPosts, QuestionPostViewModel>();
        }
    }
}
