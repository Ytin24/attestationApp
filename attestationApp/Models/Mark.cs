using System;
using System.Collections.Generic;

namespace attestationApp.Models;

public partial class Mark
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();
}
