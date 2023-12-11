﻿using AreaBox_V0._1.Data.Model;

namespace AreaBox_V0._1.Areas.Admin.Models.QuestionPostsReports;

public class QuestionPostsReportViewModel
{
    public int PostReportId { get; set; }

    public string QpostId { get; set; }

    public string UserId { get; set; }

    public ApplicationUser User { get; set; }

    public virtual QuestionPosts Qpost { get; set; }

    public virtual PostReports PostReports { get; set; }
}