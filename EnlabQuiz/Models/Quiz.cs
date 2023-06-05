using System;
using System.Collections.Generic;

namespace EnlabQuiz.Models;

public partial class Quiz
{
    public int Id { get; set; }

    public string? Question { get; set; }

    public string? Answer { get; set; }

    public string? Correct { get; set; }

    public virtual ICollection<HistoryPlay> HistoryPlays { get; set; } = new List<HistoryPlay>();
}
