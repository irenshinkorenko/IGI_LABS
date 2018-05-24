using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class DoctorController : Controller
    {       
            HospitalContext db;

            public DoctorController(HospitalContext context)
            {
                db = context;
            }

            private void LoadSessionObject()
            {
                // Load session by Extension method.
                string value = HttpContext.Session.Get<string>("DoctorName");
                ViewData["nameSession"] = value;

                value = HttpContext.Session.Get<string>("DoctorOptionOne");
                ViewData["OptionOne"] = value;

                value = HttpContext.Session.Get<string>("DoctorOptionTwo");
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
            var doc = db.Doctors.Where(a => a.Id.ToString() == key).FirstOrDefault();
            if (doc != null)
            {
                ViewBag.Name = doc.FullName;               
                ViewBag.ID = doc.Id;
                ViewBag.Departments = new SelectList(db.Departments.ToArray());
            }
            return View(); // done to this point
        }

        [Authorize(Roles = "admin")]
        public IActionResult EditEntry(SomeData entry)
        {
            var doc = db.Doctors.Where(a => a.Id.ToString() == entry.Data1).FirstOrDefault();
            var depId = db.Departments.Where(a => a.Name == entry.Data5).First().DepartmentId;
            if (entry != null)
            {
                doc.FullName = entry.Data2;
                doc.DepartmentId = depId;
            }
            db.SaveChanges();
            return View("Index");
        }

        public IActionResult GetDepartments()
        {
            return Json(db.Departments.ToList());
        }

        [Authorize(Roles = "admin")]
            public IActionResult AddEntry(SomeData entry)
            {
                var depId = db.Departments.Where(a => a.Name == entry.Data4).First().DepartmentId;            
                db.Doctors.Add(new Lab5.Models.Doctor { FullName = entry.Data1, DepartmentId = depId });
                // сделать проверку нормального числа
                db.SaveChanges();
                return View("Index");
            }

            [Authorize(Roles = "admin")]
            public IActionResult Del(SomeData entries)
            {
                 string[] IDs = entries.Data1.Substring(0, entries.Data1.Length - 1).Split(";");

                 foreach (string s in IDs) // test this
                 {
                     db.Doctors.Remove(db.Doctors.Find(int.Parse(s)));
                 }
                 db.SaveChanges();
                return View("Index"); // done to this point
            }
            

            [SessionRecordFilter]
            public IActionResult DoctorName(string name)
            {
                var selection = from r in db.Doctors
                                join b in db.Departments
                                on r.DepartmentId equals b.DepartmentId
                                where r.FullName.Contains(name)
                                orderby r.FullName descending
                                select new SomeData
                                {
                                    Data1 = r.Id.ToString(),
                                    Data2 = r.FullName,
                                    Data3 = b.Name,
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
                    var selection = from r in db.Doctors
                                    join b in db.Departments
                                    on r.DepartmentId equals b.DepartmentId
                                    
                                    orderby r.FullName ascending
                                    select new SomeData
                                    {
                                        Data1 = r.Id.ToString(),
                                        Data2 = r.FullName,
                                        Data3 = b.Name,
                                    };
                    Container.LastQuery = selection.ToArray();
                ViewBag.Count = Container.Pages;
                return PartialView("TablePartial", Divider(Container.LastQuery, 1));
                }
                else
                {
                    var selection = from r in db.Doctors
                                    join b in db.Departments
                                    on r.DepartmentId equals b.DepartmentId

                                    orderby r.FullName descending
                                    select new SomeData
                                    {
                                        Data1 = r.Id.ToString(),
                                        Data2 = r.FullName,
                                        Data3 = b.Name,
                                    };
                    Container.LastQuery = selection.ToArray();
                ViewBag.Count = Container.Pages;
                return PartialView("TablePartial", Divider(Container.LastQuery, 1));
                }
            }




            [SessionRecordFilter]
            public IActionResult GetByDepartment(int key)
            {

                if (key == 1)
                {
                    var selection = from r in db.Doctors
                                    join b in db.Departments
                                    on r.DepartmentId equals b.DepartmentId

                                    orderby r.DepartmentId ascending
                                    select new SomeData
                                    {
                                        Data1 = r.Id.ToString(),
                                        Data2 = r.FullName,
                                        Data3 = b.Name,
                                    };
                    Container.LastQuery = selection.ToArray();
                ViewBag.Count = Container.Pages;
                return PartialView("TablePartial", Divider(Container.LastQuery, 1));
                }
                else
                {
                    var selection = from r in db.Doctors
                                    join b in db.Departments
                                    on r.DepartmentId equals b.DepartmentId

                                    orderby r.DepartmentId descending
                                    select new SomeData
                                    {
                                        Data1 = r.Id.ToString(),
                                        Data2 = r.FullName,
                                        Data3 = b.Name,
                                    };
                    Container.LastQuery = selection.ToArray();
                ViewBag.Count = Container.Pages;
                return PartialView("TablePartial", Divider(Container.LastQuery, 1));
                }
            }

        protected IEnumerable<SomeData> Divider(IEnumerable<SomeData> query, int page)
        {
            List<SomeData> onePage = new List<SomeData>();
            int Pages = Container.Pages;
            int endPosition = (page == Container.Pages) ? Container.DataCount - 1 :
                                                          (page * 10) - 1;


            int startPosition = (page == Container.Pages) ? (page-1) * 10 :
                                                            endPosition - 9;

            if (page == 1)
            {
                startPosition = 0;
                if(Container.Pages == 0)
                endPosition = Container.DataCount-1;
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