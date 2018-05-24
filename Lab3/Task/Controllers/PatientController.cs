using System;
using System.Collections.Generic;
using System.Linq;
using Application.Controllers;
using Application.Models;
using Lab4;
using Lab4.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab5.Controllers
{
    
    [TypeFilter(typeof(TimingLogAttribute))]
    [ExceptionFilter]
    public class PatientController : Controller
    {
        HospitalContext db;

        public PatientController(HospitalContext context)
        {
            db = context;
        }

        private void LoadSessionObject()
        {
            // Load session by Extension method.
            string value = HttpContext.Session.Get<string>("PatientName");
            ViewData["nameSession"] = value;

            value = HttpContext.Session.Get<string>("PatientOptionOne");
            ViewData["OptionOne"] = value;

            value = HttpContext.Session.Get<string>("PatientOptionTwo");
            ViewData["OptionTwo"] = value;
        }

        [HttpGet]
        public IActionResult Index()
        {
            LoadSessionObject();
            return View();
        }

        [Authorize(Roles = "admin")]
        public IActionResult Edit(string key)
        {
            var pat = db.Patients.Where(a => a.Id.ToString() == key).FirstOrDefault();
            if (pat != null)
            {
                ViewBag.Name = pat.FullName;
                ViewBag.DateRec = pat.DateReceipt.ToShortDateString();
                ViewBag.DateDisc = pat.DateDischarge.ToShortDateString();
                ViewBag.ID = pat.Id;
                ViewBag.Doctors = new SelectList(db.Doctors.ToArray());
            }
            return View(); 
        }

        [Authorize(Roles = "admin")]
        public IActionResult EditEntry(SomeData entry)
        {
            var pat = db.Patients.Where(a => a.Id.ToString() == entry.Data1).FirstOrDefault();
            var docId = db.Doctors.Where(a => a.FullName == entry.Data5).First().Id;
            if (entry != null)
            {
                pat.FullName = entry.Data2;
                pat.DateDischarge = DateTime.Parse(entry.Data3);
                pat.DateReceipt = DateTime.Parse(entry.Data4);
                pat.DoctorId = docId;
            }
            db.SaveChanges();
            return View("Index"); 
        }

        [Authorize(Roles = "admin")]
        public IActionResult AddEntry(SomeData entry)
        {
            var DocId = db.Doctors.Where(a => a.FullName == entry.Data4).First().Id;
            db.Patients.Add(new Lab5.Models.Patient { FullName = entry.Data1, DoctorId = DocId, DateReceipt = DateTime.Parse(entry.Data3), DateDischarge = DateTime.Parse(entry.Data2) });
           
            db.SaveChanges();
            return View("Index"); 
        }

        [Authorize(Roles = "admin")]
        public IActionResult Del(SomeData entries)
        {
            string[] IDs = entries.Data1.Substring(0, entries.Data1.Length - 1).Split(";");

            foreach (string s in IDs) 
            {
                db.Patients.Remove(db.Patients.Find(int.Parse(s)));
            }
            
            db.SaveChanges();
            return View("Index");
        }


        [SessionRecordFilter]
        public IActionResult PatientName(string name)
        {
            var selection = from r in db.Patients
                            join b in db.Doctors
                            on r.DoctorId equals b.Id
                            where r.FullName.Contains(name)
                            orderby r.FullName descending
                            select new SomeData
                            {
                                Data1 = r.Id.ToString(),
                                Data2 = r.FullName,
                                Data3 = b.FullName,
                                Data4 = r.DateDischarge.ToShortDateString(),
                                Data5 = r.DateReceipt.ToShortDateString()
                            };
            Container.LastQuery = selection.ToArray();
            ViewBag.Count = Container.Pages;
            return PartialView("TablePartial", Divider(Container.LastQuery, 1));
        }



        [HttpGet]
        [SessionRecordFilter]
        public IActionResult GetByName(int key)
        {

            if (key == 1)
            {
                var selection = from r in db.Patients
                                join b in db.Doctors
                                on r.DoctorId equals b.Id                                
                                orderby r.FullName ascending
                                select new SomeData
                                {
                                    Data1 = r.Id.ToString(),
                                    Data2 = r.FullName,
                                    Data3 = b.FullName,
                                    Data4 = r.DateDischarge.ToShortDateString(),
                                    Data5 = r.DateReceipt.ToShortDateString()
                                };
                Container.LastQuery = selection.ToArray();
                ViewBag.Count = Container.Pages;
                return PartialView("TablePartial", Divider(Container.LastQuery, 1));
            }
            else
            {
                var selection = from r in db.Patients
                                join b in db.Doctors
                                on r.DoctorId equals b.Id
                                orderby r.FullName descending
                                select new SomeData
                                {
                                    Data1 = r.Id.ToString(),
                                    Data2 = r.FullName,
                                    Data3 = b.FullName,
                                    Data4 = r.DateDischarge.ToShortDateString(),
                                    Data5 = r.DateReceipt.ToShortDateString()
                                };
                Container.LastQuery = selection.ToArray();
                ViewBag.Count = Container.Pages;
                return PartialView("TablePartial", Divider(Container.LastQuery, 1));
            }
        }

        [HttpGet]
        [SessionRecordFilter]
        public IActionResult GetByDoctor(int key)
        {
            if (key == 1)
            {
                var selection = from r in db.Patients
                                join b in db.Doctors
                                on r.DoctorId equals b.Id
                                orderby r.DoctorId ascending
                                select new SomeData
                                {
                                    Data1 = r.Id.ToString(),
                                    Data2 = r.FullName,
                                    Data3 = b.FullName,
                                    Data4 = r.DateDischarge.ToShortDateString(),
                                    Data5 = r.DateReceipt.ToShortDateString()
                                };
                Container.LastQuery = selection.ToArray();
                ViewBag.Count = Container.Pages;
                return PartialView("TablePartial", Divider(Container.LastQuery, 1));
            }
            else
            {
                var selection = from r in db.Patients
                                join b in db.Doctors
                                on r.DoctorId equals b.Id
                                orderby r.DoctorId descending
                                select new SomeData
                                {
                                    Data1 = r.Id.ToString(),
                                    Data2 = r.FullName,
                                    Data3 = b.FullName,
                                    Data4 = r.DateDischarge.ToShortDateString(),
                                    Data5 = r.DateReceipt.ToShortDateString()
                                };
                Container.LastQuery = selection.ToArray();
                ViewBag.Count = Container.Pages;
                return PartialView("TablePartial", Divider(Container.LastQuery, 1));
            }
        }

        public IActionResult GetDoctors()
        {
            return Json(db.Doctors.ToList());
        }



        protected IEnumerable<SomeData> Divider(IEnumerable<SomeData> query, int page)
        {
            List<SomeData> onePage = new List<SomeData>();
            int Pages = Container.Pages;
            int endPosition = (page == Container.Pages) ? Container.DataCount - 1 :
                                                          (page * 10) - 1;


            int startPosition = (page == Container.Pages) ? (page - 1) * 10 :
                                                            endPosition - 9;

            if (page == 1)
            {
                startPosition = 0;
                if (Container.Pages == 0)
                    endPosition = Container.DataCount - 1;
                else
                {
                    endPosition = 9;
                }
            }

            for (int i = startPosition; i <= endPosition; i++)
            {
                onePage.Add(query.ElementAt(i));
            }
            return onePage;
        }


        public IActionResult GetPage(int page)
        {
            ViewBag.Count = Container.Pages;
            return PartialView("TablePartial", Divider(Container.LastQuery, page));
        }
    }
}