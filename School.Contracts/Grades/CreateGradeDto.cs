namespace School.Contracts.Grades;

public class CreateGradeDto
{
    public int StudentId { get; set; }
    public int TeacherId { get; set; }
    public string ExamName { get; set; }
    public double Score { get; set; }
    public DateTime ExamDate { get; set; }
}
