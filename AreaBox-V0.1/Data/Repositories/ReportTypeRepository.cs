using AreaBox_V0._1.Data.Interface;
using AreaBox_V0._1.Data.Model;
using AutoMapper;

namespace AreaBox_V0._1.Data.Repositories
{
    public class ReportTypeRepository : Repository<ReportTypes>, IReportTypeRepository
    {
        private readonly AreaBoxDbContext _db;
        private readonly IMapper _mapper;

        public ReportTypeRepository(AreaBoxDbContext db, IMapper mapper) : base(db, mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<T> GetPostByReportId<T>(Guid id)
        {
            return default;
        }

    }
}
