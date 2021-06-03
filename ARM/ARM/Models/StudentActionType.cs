using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARM.Models.Base;

namespace ARM.Models
{
    public class StudentActionType: EntityBase
    {
        public string Name { get; set; }

        public IList<StudentAction> Actions { get; set; }
    }
}
