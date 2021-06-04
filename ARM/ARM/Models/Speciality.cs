using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARM.Models.Base;

namespace ARM.Models
{
    public class Speciality: EntityBase
    {
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int LearnMonth { get; set; }
        public bool IsActual { get; set; }
        public double Cost { get; set; }
        public IList<Group> Groups { get; set; }
    }
}
