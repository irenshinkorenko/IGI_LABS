using System;
using System.Linq;
using lab1.Utils;
using lab1.Models;

namespace lab1.Data
{
    public static class DbInitializer
    {
        public static void Initialize(HospitalContext db)
        {
            db.Database.EnsureCreated();

            int departmentsCount = 40;
            int doctorsCount = 40;
            int patientsCount = 100;

            InitializeDepartments(db, departmentsCount);
            InitializeDoctors(db, doctorsCount, departmentsCount);
            InitializePatients(db, patientsCount, doctorsCount);
        }

        private static void InitializeDepartments(HospitalContext db, int departmentsCount)
        {
            db.Database.EnsureCreated();

            // Check if table Tours has elements
            if (db.Departments.Any())
            {
                return;   // Database was initialised
            }

            string name;
            string quantityPlaces;
            
            Random randObj = new Random(1);
            for (int id = 1; id <= departmentsCount; id++)
            {
                name = MyRandom.RandomString(5);
                quantityPlaces = MyRandom.RandomString(10);
                
                db.Departments.Add(new Department
                {
                    Id = id,
                     Name = name,
                     QuantityPlaces = quantityPlaces
                });           
            }

            //save changes in database
            db.SaveChanges();
        }

        private static void InitializeDoctors(HospitalContext db, int doctorsCount, int departmentsCount)
        {
            db.Database.EnsureCreated();

            // Check if table Clients has elements
            if (db.Doctors.Any())
            {
                return;   // Database was initialised
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
                    Speciality = speciality,
                    Category = category,
                    DepartmentId = departmentId
                });
            }

            //save changes in database
            db.SaveChanges();
        }

        private static void InitializePatients(HospitalContext db, int patientsCount, int doctorsCount)
        {
            db.Database.EnsureCreated();

            // Check if table TourKinds has elements
            if (db.Patients.Any())
            {
                return;   // Database was initialised
            }
            
            string fullName;
            string address;
            string word;
            string diagnosis;
            DateTime dateReceipt;
            DateTime dateDischarge;

            int doctorId;

        Random randObj = new Random(1);
            for (int id = 1; id <= patientsCount; id++)
            {
                fullName = MyRandom.RandomString(randObj.Next(5, 10));
                address = MyRandom.RandomString(randObj.Next(5, 7));
                word = MyRandom.RandomString(randObj.Next(5, 10));
                diagnosis = MyRandom.RandomString(randObj.Next(5, 15));

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
                    Address = address,
                    Word = word,
                    Diagnosis = diagnosis,
                    DateDischarge = dateDischarge,
                    DateReceipt = dateReceipt,
                    DoctorId = doctorId
                });
            }

            //save changes in database
            db.SaveChanges();
        }
    }

}
