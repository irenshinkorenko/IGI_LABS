using System;
using System.Linq;
using Lab5.Models;
using Application.Models;
using Lab5.Utils;

namespace Lab5.Data
{
    public static class DbInitializer
    {
        public static void Initialize(HospitalContext db)
        {
            db.Database.EnsureCreated();

            int departmentsCount = 40;
            int doctorsCount = 100;
            int patientsCount = 1000;

            InitializeDepartments(db, departmentsCount);
            InitializeDoctors(db, doctorsCount, departmentsCount);
            InitializePatients(db, patientsCount, doctorsCount);
        }

        private static void InitializeDepartments(HospitalContext db, int departmentsCount)
        {
            db.Database.EnsureCreated();

            
            if (db.Departments.Any())
            {
                return;   
            }

            string name;
            int quantityPlaces;
            
            Random randObj = new Random(1);
            for (int id = 1; id <= departmentsCount; id++)
            {
                name = MyRandom.RandomString(5);
                quantityPlaces = randObj.Next(3, 10);
                
                db.Departments.Add(new Department
                {                     
                     Name = name,
                     QuantityPlaces = quantityPlaces
                });           
            }          
            db.SaveChanges();
        }

        private static void InitializeDoctors(HospitalContext db, int doctorsCount, int departmentsCount)
        {
            db.Database.EnsureCreated();

            
            if (db.Doctors.Any())
            {
                return;   
            }

            string fullName;
            string speciality;
            int category;
            int departmentId;

            Random randObj = new Random(1);
            for (int id = 1; id <= doctorsCount; id++)
            {
                fullName = MyRandom.RandomString(10);
                speciality = MyRandom.RandomString(10);

                category = randObj.Next(1, 10);
                departmentId = randObj.Next(1, departmentsCount-1);

                db.Doctors.Add(new Doctor
                {
                    FullName = fullName,
                    DepartmentId = departmentId
                });
            }
           
            db.SaveChanges();
        }

        private static void InitializePatients(HospitalContext db, int patientsCount, int doctorsCount)
        {
            db.Database.EnsureCreated();
            
            if (db.Patients.Any())
            {
                return;   
            }
            
            string fullName;            
            DateTime dateReceipt;
            DateTime dateDischarge;

            int doctorId;

        Random randObj = new Random(1);
            for (int id = 1; id <= patientsCount; id++)
            {
                fullName = MyRandom.RandomString(randObj.Next(5, 10));
               
                dateReceipt = new DateTime(randObj.Next(1990, 2016),
                   randObj.Next(1, 12),
                    randObj.Next(1, 28));

                dateDischarge = new DateTime(randObj.Next(1990, 2016),
                   randObj.Next(1, 12),
                    randObj.Next(1, 28));

                doctorId = randObj.Next(1, doctorsCount-1);

                db.Patients.Add(new Patient
                {
                    FullName = fullName,
                    DateDischarge = dateDischarge,
                    DateReceipt = dateReceipt,
                    DoctorId = doctorId
                });
            }

           
            db.SaveChanges();
        }
    }

}
