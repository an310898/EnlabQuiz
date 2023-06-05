using System;
using System.Collections.Generic;

namespace EnlabQuiz.Models;

public partial class PlayQuiz
{
    public int Id { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public virtual ICollection<HistoryPlay> HistoryPlays { get; set; } = new List<HistoryPlay>();
}
