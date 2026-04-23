using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Contracts.Teachers;

public class CreateTeacherDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int SubjectId { get; set; }
}

