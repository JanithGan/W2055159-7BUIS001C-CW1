namespace SmartStudyApp.Models;

public class ReportRecord
{
    public int Number { get; set; }
    public string Type { get; set; }

    public string Module { get; set; }

    public string StartTime { get; set; }

    public string EndTime { get; set; }

    public bool IsRecurrent { get; set; }

    public string Notes { get; set; }
}