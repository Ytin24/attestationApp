using System;
using System.Collections.Generic;

namespace attestationApp.Models;

public partial class Direction
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
