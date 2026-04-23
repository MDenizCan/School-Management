using School.Domain.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Entities;

public class Teacher : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public int SubjectId { get; set; }
    public Subject Subject { get; set; }

    public ICollection<Grade> Grades { get; set; } = new List<Grade>();
}

