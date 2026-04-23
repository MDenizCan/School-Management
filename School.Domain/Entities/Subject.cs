using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Domain.Common;

namespace School.Domain.Entities;

public class Subject : BaseEntity
{
    public string Name { get; set; }

    public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}
