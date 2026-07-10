using System;
using System.Collections.Generic;

namespace S26W9DbFirstApproach;

public partial class Teacher
{
    public int TeacherId { get; set; }

    public string? TeacherName { get; set; }

    public int? StandardId { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual Standard? Standard { get; set; }
}
