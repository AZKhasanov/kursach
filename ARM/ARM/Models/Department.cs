using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARM.Models.Base;

namespace ARM.Models
{
    public class Department: EntityBase
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; }
        public IList<Speciality> Specialities { get; set; }
        public IList<Employee> Employees { get; set; }
    }
}
