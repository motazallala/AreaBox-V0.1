﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace AreaBox_V0._1.Models;

public partial class UsersQusetionPostComments
{
    public string QpcommentId { get; set; }

    public string UserId { get; set; }

    public virtual QuestionPostComments Qpcomment { get; set; }

    public virtual AspNetUsers User { get; set; }
}