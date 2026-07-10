using System;
using System.Collections.Generic;

namespace S26W9DbFirstApproach;

public partial class StudentAddress
{
    public int StudentId { get; set; }

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public string? City { get; set; }

    public string? Province { get; set; }

    public virtual Student Student { get; set; } = null!;
}
