using System;

namespace Application.ViewModels
{
    public class DoctorViewModel
    {        
        public int DoctorID { get; set; }
            
        
        public string DoctorName { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }
        public string PatientName { get; set; }
    }
}
