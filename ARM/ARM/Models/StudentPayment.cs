using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARM.Models.Base;

namespace ARM.Models
{
    public class StudentPayment: EntityBase
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
