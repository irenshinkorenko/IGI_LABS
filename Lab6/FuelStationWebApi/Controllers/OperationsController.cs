using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Application.Models;
using Microsoft.EntityFrameworkCore;
using Application.ViewModels;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    public class OperationsController : Controller
    {
        private readonly HospitalContext _context;
        public OperationsController(HospitalContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        [Produces("application/json")]
        public List<DoctorViewModel> Get()
        {
            var ovm = _context.Doctors.Include(t => t.Department).Select(o=> 
                new DoctorViewModel
                {
                    DepartmentId = o.DepartmentId,
                    DoctorID = o.Id,
                    DoctorName = o.FullName
                });
            return ovm.OrderByDescending(t=>t.DoctorID).Take(20).ToList();
        }
       
        [HttpGet("patients")]
        [Produces("application/json")]
        public IEnumerable<Patient> GetPatients()
        {
            return _context.Patients.ToList();
        }
       
        [HttpGet("departments")]
        [Produces("application/json")]
        public IEnumerable<Department> GetDepartments()
        {
            return _context.Departments.ToList();
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Doctor operation = _context.Doctors.FirstOrDefault(x => x.Id == id);
            if (operation == null)
                return NotFound();
            return new ObjectResult(operation);
        }
        
        [HttpPost]
        public IActionResult Post([FromBody]Doctor operation)
        {
            if (operation == null)
            {
                return BadRequest();
            }

            _context.Doctors.Add(operation);
            _context.SaveChanges();
            return Ok(operation);
        }

        
        [HttpPut]
        public IActionResult Put([FromBody]Doctor operation)
        {
            if (operation == null)
            {
                return BadRequest();
            }
            if (!_context.Doctors.Any(x => x.Id == operation.Id))
            {
                return NotFound();
            }

            _context.Update(operation);
            _context.SaveChanges();


            return Ok(operation);
        }

        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Doctor operation = _context.Doctors.FirstOrDefault(x => x.Id == id);
            if (operation == null)
            {
                return NotFound();
            }
            _context.Doctors.Remove(operation);
            _context.SaveChanges();
            return Ok(operation);
        }
    }
}
