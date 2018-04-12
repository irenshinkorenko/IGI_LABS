using System;
using System.Collections.Generic;
using System.Text;

namespace lab1.Models
{
   public class Doctor
    {
        public Doctor()
        {
            this.Patients = new List<Patient>();
        }


        public int Id { get; set; }
        public string FullName { get; set; }
        public string Speciality { get; set; }
        public int Category { get; set; }

        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public virtual IEnumerable<Patient> Patients { get; set; }

        public override string ToString()
        {
            return "{ Id = " + Id +
                   ", FullName = " + FullName +
                    ", Speciality = " + Speciality +
                     ", Category = " + Category +
                   ", DepartmentId = " + DepartmentId + " }";
        }
    }
}
