using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARM.Models.Base;

namespace ARM.Models
{
    public class Employee: EntityBase
    {
        public int EmployeeTypeID { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string FullName => $"{this.LastName} {this.Name} {this.MiddleName}";
        public IList<Group> Groups { get; set; }
    }
}
