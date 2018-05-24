using System;
using System.Collections.Generic;
using System.Text;

namespace Lab5.Models
{
   public class Doctor
    {
        public Doctor()
        {
            this.Patients = new List<Patient>();
        }


        public int Id { get; set; }
        public string FullName { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public virtual IEnumerable<Patient> Patients { get; set; }

        public override string ToString()
        {
            return FullName;
        }
    }
}
