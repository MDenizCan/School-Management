using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Domain.Common;

namespace School.Domain.Entities;

public class Grade : BaseEntity
{
    public int StudentId { get; set; }
    public Student Student { get; set; }

    public int TeacherId { get; set; }
    public Teacher Teacher { get; set; }

    public string ExamName { get; set; }
    public double Score { get; set; }
    public DateTime ExamDate { get; set; }
}

