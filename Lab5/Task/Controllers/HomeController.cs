using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lab4;
using Lab4.Filters;
using Microsoft.AspNetCore.Authorization;


namespace Application.Controllers
{
    public static class Container
    {
        private static IEnumerable<SomeData> lastQuery;

        public static IEnumerable<SomeData> LastQuery { get => lastQuery; set => lastQuery = value; }
        public static int Pages { get => lastQuery.Count() / 10; }
        public static int DataCount { get => lastQuery.Count(); }
    }

    public class SomeData
    {
        string data1;
        string data2;
        string data3;
        string data4;
        string data5;
        string data6;

        public string Data1 { get => data1; set => data1 = value; }
        public string Data2 { get => data2; set => data2 = value; }
        public string Data4 { get => data4; set => data4 = value; }
        public string Data3 { get => data3; set => data3 = value; }
        public string Data5 { get => data5; set => data5 = value; }
        public string Data6 { get => data6; set => data6 = value; }
    }

    [Authorize(Roles = "user, admin")]
    [TypeFilter(typeof(TimingLogAttribute))]
    [ExceptionFilter]
    public class HomeController : Controller
    {
        HospitalContext db;

        public HomeController(HospitalContext context)
        {
            db = context;
        }

        private void LoadSessionObject()
        {
            // Load session by Extension method.
            string value = HttpContext.Session.Get<string>("Name");
            ViewData["nameSession"] = value;

            value = HttpContext.Session.Get<string>("OptionOne");
            ViewData["OptionOne"] = value;

            value = HttpContext.Session.Get<string>("OptionTwo");
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
            var depart = db.Departments.Where(a => a.DepartmentId.ToString() == key).FirstOrDefault();
            if(depart != null)
            {
                ViewBag.Name = depart.Name;
                ViewBag.Quant = depart.QuantityPlaces;
                ViewBag.ID = depart.DepartmentId;
            }
            return View();
        }

        [Authorize(Roles = "admin")]
        public IActionResult EditEntry(SomeData entry)
        {
            var depart = db.Departments.Where(a => a.DepartmentId.ToString() == entry.Data3).FirstOrDefault();
            if(entry != null)
            {
                depart.Name = entry.Data1;
                depart.QuantityPlaces = int.Parse(entry.Data2);
            }
            db.SaveChanges();
            return View("Index");
        }

        [Authorize(Roles = "admin")]
        public IActionResult AddEntry(SomeData entry)
        {
            db.Departments.Add(new Lab5.Models.Department { Name = entry.Data1, QuantityPlaces = int.Parse(entry.Data2) });
            
            db.SaveChanges();
            return View("Index");
        }
        [Authorize(Roles = "admin")]
        public IActionResult Del(SomeData entries)
        {
            string[] IDs = entries.Data1.Substring(0, entries.Data1.Length - 1).Split(";");

            foreach (string s in IDs) 
            {
                db.Departments.Remove(db.Departments.Find(int.Parse(s)));
            }         
            db.SaveChanges();
            return View("Index");
        }




        [SessionRecordFilter]
        public IActionResult DepartmentName(string name)
        {
            var selection = from r in db.Departments
                            where r.Name.Contains(name)
                            orderby r.Name descending
                            select new SomeData
                            {
                                Data1 = r.DepartmentId.ToString(),
                                Data2 = r.Name,
                                Data3 = r.QuantityPlaces.ToString(),

                            };
            Container.LastQuery = selection.ToArray();
            ViewBag.Count = Container.Pages;
            return PartialView("TablePartial", Divider(Container.LastQuery, 1));
        }



        [HttpGet]

        [SessionRecordFilter]
        public IActionResult GetByQuantity(int key)
        {

            if (key == 1)
            {
                var selection = from r in db.Departments
                                orderby r.QuantityPlaces ascending
                                select new SomeData
                                {
                                    Data1 = r.DepartmentId.ToString(),
                                    Data2 = r.Name,
                                    Data3 = r.QuantityPlaces.ToString(),

                                };
                Container.LastQuery = selection.ToArray();
                ViewBag.Count = Container.Pages;
                return PartialView("TablePartial", Divider(Container.LastQuery, 1));
            }
            else
            {
                var selection = from r in db.Departments
                                orderby r.QuantityPlaces descending
                                select new SomeData
                                {
                                    Data1 = r.DepartmentId.ToString(),
                                    Data2 = r.Name,
                                    Data3 = r.QuantityPlaces.ToString(),

                                };
                Container.LastQuery = selection.ToArray();
                ViewBag.Count = Container.Pages;
                return PartialView("TablePartial", Divider(Container.LastQuery, 1));
            }
        }




        [SessionRecordFilter]
        public IActionResult GetByName(int key)
        {

            if (key == 1)
            {
                var selection = from r in db.Departments
                                orderby r.Name ascending
                                select new SomeData
                                {
                                    Data1 = r.DepartmentId.ToString(),
                                    Data2 = r.Name,
                                    Data3 = r.QuantityPlaces.ToString(),

                                };
                Container.LastQuery = selection.ToArray();
                ViewBag.Count = Container.Pages;
                return PartialView("TablePartial", Divider(Container.LastQuery, 1));
            }
            else
            {
                var selection = from r in db.Departments
                                orderby r.Name descending
                                select new SomeData
                                {
                                    Data1 = r.DepartmentId.ToString(),
                                    Data2 = r.Name,
                                    Data3 = r.QuantityPlaces.ToString(),

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
