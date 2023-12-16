using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
using AutoMapper;

namespace AreaBox_V0._1.Data.Repositories
{
    public class UserManagementRepository : Repository<ApplicationUser>, IUserManagement
    {
        private readonly AreaBoxDbContext _db;
        private readonly IMapper _mapper;

        public UserManagementRepository(AreaBoxDbContext db, IMapper mapper) : base(db, mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task Disable(string id, bool state)
        {
            var getUser = await _db.Users.FindAsync(id);

            if (getUser != null)
            {
                getUser.State = !state;
                _db.Users.Update(getUser);
            }
        }
    }
}
