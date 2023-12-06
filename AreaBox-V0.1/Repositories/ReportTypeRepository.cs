using AreaBox_V0._1.Data.Model;
using AreaBox_V0._1.Interface;
using AutoMapper;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AreaBox_V0._1.Repositories
{
    public class ReportTypeRepository : IReportType
    {
        private readonly AreaBoxDbContext _db;
        private readonly IMapper _mapper;
        public ReportTypeRepository(AreaBoxDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<T> GetPostByReportId<T>(Guid id)
        {
            var report = await _db.ReportTypes.FindAsync(id);

            if (report != null)
            {
                var searchQAReport = await _db.QuestionPostsReports.FindAsync(report.ReportTypeId);
                var searchMPReport = await _db.MediaPostsReports.FindAsync(report.ReportTypeId);

                if (searchQAReport != null && searchMPReport == null)
                {
                    var getQAPost = await _db.QuestionPosts.FindAsync(searchQAReport.QpostId);
                    return _mapper.Map<T>(getQAPost);
                }
                else if (searchMPReport != null && searchQAReport == null)
                {
                    var getMPost = await _db.MediaPosts.FindAsync(searchMPReport.MpostId);
                    return _mapper.Map<T>(getMPost);
                }
            }

            return default;
        }

    }
}
