namespace School.Contracts.Grades;

public class TranscriptDto
{
    public int StudentId { get; set; }
    public string StudentFullName { get; set; }
    public List<SubjectAverageDto> SubjectAverages { get; set; } = new();
}
