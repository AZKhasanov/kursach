using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARM.Models.Base;

namespace ARM.Models
{
    public class Faculty: EntityBase
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public IList<Department> Departments { get; set; }
    }
}
