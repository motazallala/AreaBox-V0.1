using AreaBox_V0._1.Data.Model;

namespace AreaBox_V0._1.Models
{
    public class QuestionPost
    {
        public Guid Id { get; set; }

        public int CategoryId { get; set; }

        public int CityId { get; set; }

        public DateTime Date { get; set; }

        public string UserId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool State { get; set; }

        public virtual Categories Category { get; set; }

        public virtual Cities City { get; set; }

        public virtual ApplicationUser User { get; set; }  

        public virtual ICollection<QuestionPostComments> QuestionPostComments { get; set; } = new List<QuestionPostComments>();
    }
}
