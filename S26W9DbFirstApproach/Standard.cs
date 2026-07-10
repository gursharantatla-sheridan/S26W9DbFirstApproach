using System;
using System.Collections.Generic;

namespace S26W9DbFirstApproach;

public partial class Standard
{
    public int StandardId { get; set; }

    public string? StandardName { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}
