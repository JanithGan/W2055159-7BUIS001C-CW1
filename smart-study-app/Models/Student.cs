namespace SmartStudyApp.Models;

public class Student
{
    public int StudentId { get; set; }
    public string Name { get; set; }
    public string StudentNumber { get; set; }
    public List<string> EnrolledModuleCodes { get; set; }
}