using Demo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ITIEntity Context;

        public EmployeeController(ITIEntity _context)
        {
            Context = _context;
        }
        [HttpGet]
        public IActionResult GetEmployee()
        {
            List<Employee> emps = Context.Employees.ToList();
            return Ok(emps);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            Employee employee = Context.Employees.FirstOrDefault(e => e.Id == id);
            return Ok(employee);


        }
        [HttpPost]
        public IActionResult PostEmployee( Employee NewEmp)
        {
            if (ModelState.IsValid)
            {
                Context.Employees.Add(NewEmp);
                Context.SaveChanges();
                return Created("http://localhost:10671/Employee/" + NewEmp.Id, NewEmp);
            }
                return BadRequest(ModelState);
        }
        [HttpPut("{id}")]
        public IActionResult PutEmployee([FromRoute] int id ,[FromBody] Employee employee)
        {
            if(ModelState.IsValid)
            {
                Employee OldEmp = Context.Employees.FirstOrDefault(e => e.Id == id);

                OldEmp.Id = employee.Id;
                OldEmp.Name = employee.Name;
                OldEmp.Salary = employee.Salary;
                OldEmp.Address = employee.Address;
                OldEmp.age = employee.age;
                Context.SaveChanges();
                return StatusCode(StatusCodes.Status204NoContent);
            }
            return BadRequest(ModelState);
        }
        [HttpDelete]
        public IActionResult DeleteEmployee( int id)
        {
            try
            {
                Employee employee = Context.Employees.FirstOrDefault(e => e.Id == id);
                Context.Employees.Remove(employee);
                Context.SaveChanges();
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
           
        }
    }
}
