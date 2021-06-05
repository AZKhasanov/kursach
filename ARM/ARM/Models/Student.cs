using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ARM.Models.Base;

namespace ARM.Models
{
    public class Student: EntityBase
    {
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsGroupHead { get; set; }

        public string FullName => $"{LastName} {Name} {MiddleName}";

        public IList<StudentAction> Actions { get; set; }
        public IList<StudentPayment> Payments { get; set; }
    }
}
