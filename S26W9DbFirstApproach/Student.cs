using System;
using System.Collections.Generic;

namespace S26W9DbFirstApproach;

public partial class Student
{
    // scalar properties
    public int StudentId { get; set; }

    public string? StudentName { get; set; }

    public int? StandardId { get; set; }


    // navigation properties
    public virtual Standard? Standard { get; set; }

    public virtual StudentAddress? StudentAddress { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
