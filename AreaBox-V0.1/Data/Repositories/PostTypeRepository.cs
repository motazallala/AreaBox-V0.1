using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
using AutoMapper;

namespace AreaBox_V0._1.Data.Repositories
{
	public class PostTypeRepository : Repository<PostType>,IPostTypeRepository
	{
		public PostTypeRepository(AreaBoxDbContext db, IMapper mapper) : base(db, mapper)
		{
		}
	}
}
