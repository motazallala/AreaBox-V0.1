using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Interface;
using AreaBox_V0._1.Models;
using Microsoft.CodeAnalysis.Elfie.Model.Strings;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace AreaBox_V0._1.Repositories
{
	public class MediaPostRepository : IMediaPost
	{
		private readonly IRepository<MediaPosts> _repoMediaPost;

		public MediaPostRepository(IRepository<MediaPosts> repoMediaPost)
		{
			_repoMediaPost = repoMediaPost;
		}

		public async Task Disable(string id, bool state)
		{
			var getMediaPost = await _repoMediaPost.GetByIdAsync(id);

			if (getMediaPost != null)
			{
				getMediaPost.Mpstate = !state;
				_repoMediaPost.Update(getMediaPost);
				await _repoMediaPost.SaveChnagesAsync();
			}
		}
	}
}
