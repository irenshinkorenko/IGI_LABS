using System;
using System.Collections.Generic;
using System.Text;

namespace lab1.Models
{
   public class Department
    {
        public Department()
        {
            this.Doctors = new List<Doctor>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string QuantityPlaces { get; set; }

        public virtual IEnumerable<Doctor> Doctors { get; set; }

        public override string ToString()
        {
            return "{ Id = " + Id +
                   ", Name = " + Name +
                   ", QuantityPlaces = " + QuantityPlaces + " }";
        }
    }
}
