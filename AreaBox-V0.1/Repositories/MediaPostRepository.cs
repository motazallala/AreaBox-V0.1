using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Interface;
using AreaBox_V0._1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Elfie.Model.Strings;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace AreaBox_V0._1.Repositories
{
	public class MediaPostRepository : IMediaPost
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IRepository<MediaPosts> _repoMediaPost;
		private readonly IRepository<MediaPostLikes> _repoMediaPostLike;

		public MediaPostRepository(UserManager<ApplicationUser> userManager, IRepository<MediaPosts> repoMediaPost, IRepository<MediaPostLikes> repoMediaPostLike)
		{
			_userManager = userManager;
			_repoMediaPost = repoMediaPost;
			_repoMediaPostLike = repoMediaPostLike;
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

		public async Task Like(string id, bool state, string currentUserID)
		{
			var getMediaPost = await _repoMediaPost.GetByIdAsync(id);

			if (getMediaPost != null)
			{
				if (state)
				{

					var newLike = new MediaPostLikes
					{
						MpostId = getMediaPost.MpostId,
						UserId = currentUserID
					};

					_repoMediaPostLike.Add(newLike);
					await _repoMediaPostLike.SaveChnagesAsync();
				}
				else
				{
					var getMedialike = _repoMediaPostLike.Find<MediaPostLikes, MediaPostLikes>(
						x => x.MpostId == getMediaPost.MpuserId && x.UserId == currentUserID);

					if (getMedialike != null)
					{
						_repoMediaPostLike.Remove(getMedialike);
						await _repoMediaPostLike.SaveChnagesAsync();
					}
				}
			}
		}
	}
}
