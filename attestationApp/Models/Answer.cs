using System;

namespace attestationApp.Models;

public partial class Answer
{
    public int Id { get; set; }

    public int? QuestionId { get; set; }

    public string Text { get; set; } = null!;

    public bool IsCorrect { get; set; }

    public virtual Question? Question { get; set; }
}
