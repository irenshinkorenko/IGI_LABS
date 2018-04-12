using System;
using System.Collections.Generic;
using System.Text;

namespace lab1.Models
{
   public class Patient
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Word { get; set;}
        public string Diagnosis { get; set; }
        public DateTime DateReceipt { get; set; }
        public DateTime DateDischarge { get; set; }

        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }

        public override string ToString()
        {
            return "{ Id = " + Id +
                   ", FullName = " + FullName +
                    ", Address = " + Address +
                     ", Word = " + Word +
                     ", Diagnosis = " + Diagnosis +
                     ", DateReceipt = " + DateReceipt +
                     ", DateDischarge = " + DateDischarge +
                   ", DateDischarge = " + DateDischarge + " }";
        }
    }
}
