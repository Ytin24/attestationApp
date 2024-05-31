using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace attestationApp.Models;

public partial class Question
{
    public int Id { get; set; }
    [Column("Question")]
    public string QuestionText { get; set; } = null!;

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
}
