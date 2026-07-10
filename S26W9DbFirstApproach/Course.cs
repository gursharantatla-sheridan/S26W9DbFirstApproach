using System;
using System.Collections.Generic;

namespace S26W9DbFirstApproach;

public partial class Course
{
    public int CourseId { get; set; }

    public string? CourseName { get; set; }

    public int? TeacherId { get; set; }

    public virtual Teacher? Teacher { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
