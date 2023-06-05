using System;
using System.Collections.Generic;

namespace EnlabQuiz.Models;

public partial class HistoryPlay
{
    public int Id { get; set; }

    public int? PlayId { get; set; }

    public int? QuizId { get; set; }

    public string? Choice { get; set; }

    public virtual PlayQuiz? Play { get; set; }

    public virtual Quiz? Quiz { get; set; }
}
