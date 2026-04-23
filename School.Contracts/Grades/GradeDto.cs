namespace School.Contracts.Grades;

public class GradeDto
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public string StudentFullName { get; set; }
    public int TeacherId { get; set; }
    public string TeacherFullName { get; set; }
    public string SubjectName { get; set; }
    public string ExamName { get; set; }
    public double Score { get; set; }
    public DateTime ExamDate { get; set; }
}
