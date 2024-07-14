using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartStudyApp.Models;

public class StudySession
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SessionId { get; set; }

    [Required] public int StudentId { get; set; }

    [Required] public string ModuleCode { get; set; }

    [Required] public DateTime StartTime { get; set; }

    [Required] public DateTime EndTime { get; set; }

    public string Notes { get; set; }

    public bool IsRecurrent { get; set; }
}