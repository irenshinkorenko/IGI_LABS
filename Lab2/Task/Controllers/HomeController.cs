using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Application.Controllers
{
    public class SomeData
    {
        string data1;
        string data2;
        string data3;
        string data4;
        string data5;

        public string Data1 { get => data1; set => data1 = value; }
        public string Data2 { get => data2; set => data2 = value; }
        public string Data4 { get => data4; set => data4 = value; }
        public string Data3 { get => data3; set => data3 = value; }
        public string Data5 { get => data5; set => data5 = value; }
    }

    public class HomeController : Controller
    {
        HospitalContext db = new HospitalContext();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NameFilter(string name)
        {
            var selection = from r in db.Doctors
                            join c in db.Departments
                            on r.DepartmentId equals c.DepartmentId
                            join a in db.Patients
                            on r.DepartmentId equals a.Id                           
                            where c.Name.Contains(name)
                            orderby c.Name descending
                            select new SomeData
                            {
                                Data1 = r.FullName.ToString(),
                                Data2 = c.Name,
                                Data3 = a.FullName,
                                Data4 = r.Id.ToString()
                            };
            return PartialView("TablePartial", selection);
        }

        public IActionResult AutoNameFilter(string name)
        {
            var selection = from r in db.Doctors
                            join c in db.Departments
                            on r.DepartmentId equals c.DepartmentId
                            join a in db.Patients
                            on r.DepartmentId equals a.Id
                            where r.FullName.Contains(name)
                            orderby c.Name descending
                            select new SomeData
                            {
                                Data1 = r.FullName.ToString(),
                                Data2 = c.Name,
                                Data3 = a.FullName,
                                Data4 = r.Id.ToString()
                            };
            return PartialView("TablePartial", selection);
        }


        public IActionResult CostFilter(int key)
        {
            if (key == 1)
            {
                var selection = from r in db.Doctors
                                join c in db.Departments
                                on r.DepartmentId equals c.DepartmentId
                                join a in db.Patients
                                on r.DepartmentId equals a.Id                                
                                orderby r.FullName ascending
                                select new SomeData
                                {
                                    Data1 = r.FullName.ToString(),
                                    Data2 = c.Name,
                                    Data3 = a.FullName,
                                    Data4 = r.Id.ToString()
                                };
                return PartialView("TablePartial", selection);
            }
            else
            {
                var selection = from r in db.Doctors
                                join c in db.Departments
                                on r.DepartmentId equals c.DepartmentId
                                join a in db.Patients
                                on r.DepartmentId equals a.Id
                                orderby r.FullName descending
                                select new SomeData
                                {
                                    Data1 = r.FullName.ToString(),
                                    Data2 = c.Name,
                                    Data3 = a.FullName,
                                    Data4 = r.Id.ToString()
                                };
                return PartialView("TablePartial", selection);
            }
        }
        [HttpGet]
        public IActionResult AutoFilter(int key)
        {
            if (key == 1)
            {
                var selection = from r in db.Doctors
                                join c in db.Departments
                                on r.DepartmentId equals c.DepartmentId
                                join a in db.Patients
                                on r.DepartmentId equals a.Id
                                orderby a.FullName ascending
                                select new SomeData
                                {
                                    Data1 = r.FullName.ToString(),
                                    Data2 = c.Name,
                                    Data3 = a.FullName,
                                    Data4 = r.Id.ToString()
                                };
                return PartialView("TablePartial", selection);
            }
            else
            {
                var selection = from r in db.Doctors
                                join c in db.Departments
                                on r.DepartmentId equals c.DepartmentId
                                join a in db.Patients
                                on r.DepartmentId equals a.Id
                                orderby a.FullName descending
                                select new SomeData
                                {
                                    Data1 = r.FullName.ToString(),
                                    Data2 = c.Name,
                                    Data3 = a.FullName,
                                    Data4 = r.Id.ToString()
                                };
                return PartialView("TablePartial", selection);
            }
        }
        
        public IActionResult GetByBrands(int key)
        {
            if (key == 1)
            {
                var selection = from r in db.Doctors
                                join c in db.Departments
                                on r.DepartmentId equals c.DepartmentId
                                join a in db.Patients
                                on r.DepartmentId equals a.Id
                                orderby c.Name ascending
                                select new SomeData
                                {
                                    Data1 = r.FullName.ToString(),
                                    Data2 = c.Name,
                                    Data3 = a.FullName,
                                    Data4 = r.Id.ToString()
                                };
                return Json(selection);
            }
            else
            {
                var selection = from r in db.Doctors
                                join c in db.Departments
                                on r.DepartmentId equals c.DepartmentId
                                join a in db.Patients
                                on r.DepartmentId equals a.Id
                                orderby c.Name descending
                                select new SomeData
                                {
                                    Data1 = r.FullName.ToString(),
                                    Data2 = c.Name,
                                    Data3 = a.FullName,
                                    Data4 = r.Id.ToString()
                                };
                return Json(selection);
            }
        }



        public IActionResult ClientFilter(int key)
        {
            if (key == 1)
            {
                var selection = from r in db.Doctors
                                join c in db.Departments
                                on r.DepartmentId equals c.DepartmentId
                                join a in db.Patients
                                on r.DepartmentId equals a.Id
                                orderby c.Name descending
                                select new SomeData
                                {
                                    Data1 = r.FullName.ToString(),
                                    Data2 = c.Name,
                                    Data3 = a.FullName,
                                    Data4 = r.Id.ToString()
                                };
                return PartialView("TablePartial", selection);
            }
            else
            {
                var selection = from r in db.Doctors
                                join c in db.Departments
                                on r.DepartmentId equals c.DepartmentId
                                join a in db.Patients
                                on r.DepartmentId equals a.Id
                                orderby c.Name descending
                                select new SomeData
                                {
                                    Data1 = r.FullName.ToString(),
                                    Data2 = c.Name,
                                    Data3 = a.FullName,
                                    Data4 = r.Id.ToString()
                                };
                return PartialView("TablePartial", selection);
            }
        }


        public IActionResult GetByPartName(string name)
        {
            var selection = from r in db.Doctors
                            join c in db.Departments
                            on r.DepartmentId equals c.DepartmentId
                            join a in db.Patients
                            on r.DepartmentId equals a.Id
                            orderby c.Name descending
                            select new SomeData
                            {
                                Data1 = r.FullName.ToString(),
                                Data2 = c.Name,
                                Data3 = a.FullName,
                                Data4 = r.Id.ToString()
                            };
            return Json(selection);
        }

        public IActionResult TablePartial(string name, int key)
        {
            if (key == 1) // brand
            {
                var selection = from r in db.Doctors
                                join c in db.Departments
                                on r.DepartmentId equals c.DepartmentId
                                join a in db.Patients
                                on r.DepartmentId equals a.Id
                                orderby c.Name descending
                                select new SomeData
                                {
                                    Data1 = r.FullName.ToString(),
                                    Data2 = c.Name,
                                    Data3 = a.FullName,
                                    Data4 = r.Id.ToString()
                                };
                return View(selection);
            }
            else if (key == 0) // client
            {
                var selection = from r in db.Doctors
                                join c in db.Departments
                                on r.DepartmentId equals c.DepartmentId
                                join a in db.Patients
                                on r.DepartmentId equals a.Id
                                orderby c.Name descending
                                select new SomeData
                                {
                                    Data1 = r.FullName.ToString(),
                                    Data2 = c.Name,
                                    Data3 = a.FullName,
                                    Data4 = r.Id.ToString()
                                };
                return View(selection);
            }

            return View();
        }

        public IActionResult PictureView()
        {
            return View();
        }

        public IActionResult TableView()
        {
            var selection = from r in db.Doctors
                            join c in db.Departments
                            on r.DepartmentId equals c.DepartmentId
                            join a in db.Patients
                            on r.DepartmentId equals a.Id
                            orderby c.Name ascending
                            select new SomeData
                            {
                                Data1 = r.FullName.ToString(),
                                Data2 = c.Name,
                                Data3 = a.FullName,
                                Data4 = r.Id.ToString()
                            };
            SelectList brands = new SelectList(db.Departments, "Name");
            SelectList clients = new SelectList(db.Doctors, "Name");
            ViewBag.Brands = brands;
            ViewBag.Clients = clients;
            return View(selection);
        }



        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
