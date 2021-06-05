using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARM.Models.Base;

namespace ARM.Models
{
    public class EmployeeType: EntityBase
    {
        public string Title { get; set; }

        public IList<Employee> Employees { get; set; }
    }
}
