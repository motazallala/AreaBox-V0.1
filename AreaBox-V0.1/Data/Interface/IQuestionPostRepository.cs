using AreaBox_V0._1.Data.Model;

namespace AreaBox_V0._1.Data.Interface
{
    public interface IQuestionPostRepository : IRepository<QuestionPosts>
    {
        Task Disable(string id, bool state);
    }
}
