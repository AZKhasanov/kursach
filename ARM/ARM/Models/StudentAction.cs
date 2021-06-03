using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARM.Models.Base;

namespace ARM.Models
{
    public class StudentAction: EntityBase
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int TypeId { get; set; }
        public StudentActionType Type { get; set; }
        public DateTime DateBegin { get; set; }
    }
}
