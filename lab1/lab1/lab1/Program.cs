using lab1.Data;
using lab1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Linq;

namespace lab1
{
    class Program
    {
        public const int selectCount = 5;

        static void Main(string[] args)
        {

            HospitalContext _db = new HospitalContext();
            DbInitializer.Initialize(_db);

            First(_db);
            Second(_db);

            Third(_db);
            Fourth(_db);
            Fifth(_db);
            Sixth(_db);
            Seventh(_db);
            Eight(_db);
            Nineth(_db);
            Tenth(_db);

            Console.ReadKey();
        }

        public static void Print(string caption, IEnumerable items)
        {
            Console.WriteLine(caption);
            foreach (var item in items)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        public static void First(HospitalContext _db)
        {
            string message = "Select all departments";
            var departments = _db.Departments
                .OrderBy(d => d.Name)
                .Take(selectCount).ToList();

            Print(message, departments);
        }

        public static void Second(HospitalContext _db)
        {
            string message = "Select departments by name";
            var departments = _db.Departments
                .Where(d => d.Name.Contains("z"))
                .OrderBy(d => d.Name)
                .Take(selectCount)
                .ToList();
            
            Print(message, departments);
        }

        public static void Third(HospitalContext _db)
        {
            string message = "Select doctors group by departments name";
            var departments = _db.Doctors
                .Include(t => t.Department)
                .GroupBy(t => t.Department.Name, t => t.Category)
                .Select(gr => new
                {
                    DepartmentName = gr.Key,
                    AverageCategory = gr.Average()
                })
                .OrderBy(t => t.DepartmentName)
                .Take(selectCount)
                .ToList();

            Print(message, departments);
        }

        public static void Fourth(HospitalContext _db)
        {
            string message = "Select doctors";
            var doctors = _db.Doctors
                .Include(t => t.Department)
                .Select(doctor => new
                {
                    Id = doctor.Id,
                    FullName = doctor.FullName,
                    Speciality = doctor.Speciality,
                    Category = doctor.Category,
                    DepartmentName = doctor.Department.Name,
                    DepartmentQuantityPlaces = doctor.Department.QuantityPlaces
                })
                .OrderBy(t => t.FullName)
                .Take(selectCount)
                .ToList();

            Print(message, doctors);
        }

        public static void Fifth(HospitalContext _db)
        {
            string message = "Select doctors by department name";
            var doctors =  _db.Doctors
                .Include(t => t.Department)
                .Select(doctor => new
                {
                    Id = doctor.Id,
                    FullName = doctor.FullName,
                    Speciality = doctor.Speciality,
                    Category = doctor.Category,
                    DepartmentName = doctor.Department.Name,
                    DepartmentQuantityPlaces = doctor.Department.QuantityPlaces
                })
                .Where(t => t.DepartmentName.Contains("a"))
                .OrderBy(t => t.FullName)
                .Take(selectCount)
                 .ToList();

            Print(message, doctors);
        }

        public static void Sixth(HospitalContext _db)
        {
            string message = "Insert department";
            var dep = new Department
            {
                Name = "New Dep",
                QuantityPlaces = "QuantityPlaces"
            };

            _db.Departments.Add(dep);
            _db.SaveChanges();


            var departments = _db.Departments
                 .OrderBy(t => t.Name)
                .Take(selectCount)
                 .ToList();

            Print(message, departments);
        }

        public static void Seventh(HospitalContext _db)
        {
            string message = "Insert doctor";
            var dep = new Department
            {
                Name = "New Dep",
                QuantityPlaces = "QuantityPlaces"
            };

            _db.Departments.Add(dep);
            _db.SaveChanges();

            var doctor = new Doctor
            {
                FullName = "Alex",
                Speciality = "Ordinator",
                Category = 2,
                DepartmentId = dep.Id
            };

            _db.Doctors.Add(doctor);
            _db.SaveChanges();


            var doctors = _db.Doctors
                .Include(t => t.Department)
                 .OrderBy(t => t.FullName)
                .Take(selectCount)
                 .ToList();

            Print(message, doctors);
        }

        public static void Eight(HospitalContext _db)
        {
            string message = "Delete department";

            int depId = 1;

            var delPat = _db.Patients
                .Include(t => t.Doctor)
                .ThenInclude(t => t.Department)
                .Where(t => t.Doctor.DepartmentId == depId);
            _db.RemoveRange(delPat);
            _db.SaveChanges();

            var delDoc = _db.Doctors
                .Include(t => t.Department)
                .Where(t => t.DepartmentId == depId);
            _db.RemoveRange(delDoc);
            _db.SaveChanges();

            var delDep = _db.Departments.Where(t => t.Id == depId);
            _db.RemoveRange(delDep);
            _db.SaveChanges();


            var departments = _db.Departments               
                 .OrderBy(t => t.Name)
                .Take(selectCount)
                 .ToList();

            Print(message, departments);
        }

        public static void Nineth(HospitalContext _db)
        {
            string message = "Delete doctor";

            int docId = 1;

            var delPat = _db.Patients
                .Include(t => t.Doctor)
                .Where(t => t.DoctorId == docId);
            _db.RemoveRange(delPat);
            _db.SaveChanges();

            var delDoc = _db.Doctors
                .Where(t => t.Id == docId);
            _db.RemoveRange(delDoc);
            _db.SaveChanges();

           

            var doctors = _db.Doctors
                .Include(t => t.Department)
                 .OrderBy(t => t.FullName)
                .Take(selectCount)
                 .ToList();

            Print(message, doctors);
        }

        public static void Tenth(HospitalContext _db)
        {
            string message = "Update department";


            var updDepartment = _db.Departments
                .OrderBy(t => t.Name)
                .First();

            updDepartment.Name = "1111111";
            
            _db.SaveChanges();

            var departments = _db.Departments
                 .OrderBy(t => t.Name)
                .Take(selectCount)
                 .ToList();

            Print(message, departments);
        }
    }
}
