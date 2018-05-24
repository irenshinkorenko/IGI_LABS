using System;
using System.Collections.Generic;
using System.Text;

namespace Lab5.Models
{
   public class Patient
    {
        public int Id { get; set; }
        public string FullName { get; set; }
       
        public DateTime DateReceipt { get; set; }
        public DateTime DateDischarge { get; set; }

        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }

        public override string ToString()
        {
            return "{ Id = " + Id +
                   ", FullName = " + FullName +                   
                     ", DateReceipt = " + DateReceipt +
                     ", DateDischarge = " + DateDischarge +
                   ", DateDischarge = " + DateDischarge + " }";
        }
    }
}
