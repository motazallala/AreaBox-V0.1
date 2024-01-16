using AreaBox_V0._1.Data.Model;

namespace AreaBox_V0._1.Areas.User.Models.UQuestionPostDto.send
{
	public class UQuestionPostOutPutDto
	{
        public string QpostId { get; set; }

		public int QpcategoryId { get; set; }

		public int QpcityId { get; set; }

		public DateTime Qpdate { get; set; }

		public string QpuserId { get; set; }

		public string Qptitle { get; set; }

		public string Qpdescription { get; set; }

		public int? CommentCount { get; set; }

		public bool Qpstate { get; set; }

        public string UserName { get; set; }

		public string UserImage { get; set; }

        public string CountryName { get; set; }

        public string CityName { get; set; }

        public string CategoryName { get; set; }
    }
}
