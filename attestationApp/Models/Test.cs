using System;
using System.Collections.Generic;

namespace attestationApp.Models;

public partial class Test
{
    public int Id { get; set; }

    public int? OptionId { get; set; }

    public int? QuestionId { get; set; }

    public virtual Option? Option { get; set; }

    public virtual Question? Question { get; set; }

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();
}
