using System;
using System.Collections.Generic;

namespace attestationApp.Models;

public partial class Option
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
}
