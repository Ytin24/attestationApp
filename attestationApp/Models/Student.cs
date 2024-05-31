using System;
using System.Collections.Generic;

namespace attestationApp.Models;

public partial class Student
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Patronymic { get; set; }

    public DateTime Birthdate { get; set; }

    public int? GenderId { get; set; }

    public virtual Gender? Gender { get; set; }

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();

    public virtual ICollection<Direction> Directions { get; set; } = new List<Direction>();
}
