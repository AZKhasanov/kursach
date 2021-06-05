using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARM.Models.Base;

namespace ARM.Models
{
    public class Group: EntityBase
    {
        public int SpecialityId { get; set; }
        public Speciality Speciality { get; set; }
        public int? CuratorId { get; set; }
        public Employee Curator { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public DateTime DateBegin { get; set; }


        public IList<Student> Students { get; set; }
    }
}
