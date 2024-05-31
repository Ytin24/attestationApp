using System;
using System.Collections.Generic;

namespace attestationApp.Models;

public partial class Result
{
    public int Id { get; set; }

    public int? StudentId { get; set; }

    public int? TestId { get; set; }

    public int? MarkId { get; set; }

    public int? DirectionId { get; set; }

    public string? Description { get; set; }

    public virtual Direction? Direction { get; set; }

    public virtual Mark? Mark { get; set; }

    public virtual Student? Student { get; set; }

    public virtual Test? Test { get; set; }
}
