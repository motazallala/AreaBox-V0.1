namespace AreaBox_V0._1.Areas.Admin.Models.Dashboard.send
{
    public class DashboardAnalysis
    {
        public int MediaPostCount { get; set; }
        public double MediaPostsPercentage { get; set; }

        public int QuestionPostCount { get; set; }
        public double QuestionPostsPercentage { get; set; }
        public double TotalPostsPercentage { get; set; }
        public int UserCount { get; set; }

        public int LastMonthMediaPostCount { get; set; }
        public int Last3MonthsMediaPostCount { get; set; }
        public int LastMonthQuestionPostCount { get; set; }
        public int Last3MonthsQuestionPostCount { get; set; }


    }
}
