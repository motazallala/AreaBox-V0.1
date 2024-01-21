using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
using AutoMapper;

namespace AreaBox_V0._1.Data.Repositories
{
    public class UserQuestionPostSaveRepository : Repository<UserQuestionPostSave>, IUserQuestionPostSaveRepository
    {
        public UserQuestionPostSaveRepository(AreaBoxDbContext db, IMapper mapper) : base(db, mapper)
        {
        }
    }
}
