﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace AreaBox_V0._1.Models;

public partial class UsersMediaPostComments
{
    public string MpcommentId { get; set; }

    public string UserId { get; set; }

    public virtual MediaPostComments Mpcomment { get; set; }

    public virtual AspNetUsers User { get; set; }
}