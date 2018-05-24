using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models
{
   public class Department
    {
        public Department()
        {
            this.Doctors = new List<Doctor>();
        }


        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public int QuantityPlaces { get; set; }

        public virtual IEnumerable<Doctor> Doctors { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
