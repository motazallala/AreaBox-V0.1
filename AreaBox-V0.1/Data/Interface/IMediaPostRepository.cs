﻿
using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Models.Dto;
using System.Linq.Expressions;

namespace AreaBox_V0._1.Data.Interface
{
    public interface IMediaPostRepository : IRepository<MediaPosts>
    {
        Task<IEnumerable<MediaPostsDto>> FindAndFilter(Expression<Func<MediaPosts, bool>> match,
                                                                                    string[] includes = null,
                                                                                    int? cityId = null,
                                                                                    int? categoryId = null,
                                                                                    int? countryId = null,
                                                                                    int? skip = null,
                                                                                    int? take = null
                                                                                    );
        Task<int> CountMediaPost(Expression<Func<MediaPosts, bool>> match,
                                    int? cityId = null,
                                    int? categoryId = null,
                                    int? countryId = null);
        Task Disable(string id, bool state);
    }
}
