using Azure;
using EmployeeWebApi.Data;
using EmployeeWebApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EmployeeWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDbContext _employeeDbContext;
        public EmployeeController(EmployeeDbContext employeeDbContext) 
        { 
            _employeeDbContext = employeeDbContext;
        }

        [HttpPost("")]
        public async Task<IActionResult> SaveEmployeeData([FromBody] EmployeeDataModel data)
        {
            _employeeDbContext.EmployeeData.Add(data);
            await _employeeDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployeeDataById), new { EmpId = data.EmpId, controller = "employee" }, Ok("Data saved successfully"));
        }

        [HttpGet("")]
        public async Task<IActionResult> GetEmployeeData()
        {
            var employeeData = await _employeeDbContext.EmployeeData.Select(x => new EmployeeDataModel()
            {
              EmpId = x.EmpId,
              Name = x.Name,
              JobTitle = x.JobTitle,
              Department = x.Department,
              Gender = x.Gender,
              ManagerId = x.ManagerId,
              Manager = x.Manager
            }).ToListAsync();

            return Ok(employeeData);
        }

        [HttpGet("{empId}")]
        public async Task<IActionResult> GetEmployeeDataById([FromRoute]string empId)
        {
            var employeeData = await _employeeDbContext.EmployeeData.Where(x=>x.EmpId.Equals(empId)).Select(x => new EmployeeDataModel()
            {
                EmpId = x.EmpId,
                Name = x.Name,
                JobTitle = x.JobTitle,
                Department = x.Department,
                Gender = x.Gender,
                ManagerId = x.ManagerId,
                Manager = x.Manager
            }).FirstOrDefaultAsync();

            return Ok(employeeData);
        }

        [HttpPut("{empId}")]
        public async Task<IActionResult> UpdateEmployeeDataPut([FromRoute]string empId,[FromBody] EmployeeDataModel employeeUpdatedData)
        {
            var emp = await _employeeDbContext.EmployeeData.FindAsync(empId);
            if (emp != null)
            {
                emp.EmpId = employeeUpdatedData.EmpId;
                emp.Name = employeeUpdatedData.Name;
                emp.JobTitle = employeeUpdatedData.JobTitle;
                emp.Department = employeeUpdatedData.Department;
                emp.Gender = employeeUpdatedData.Gender;
                emp.ManagerId = employeeUpdatedData.ManagerId;
                emp.Manager = employeeUpdatedData.Manager;

                await _employeeDbContext.SaveChangesAsync();
                return Ok();
            }

            return BadRequest();
        }

        [HttpPatch("{empId}")]
        public async Task<IActionResult> UpdateEmployeeDataPatch([FromRoute] string empId, [FromBody] JsonPatchDocument<EmployeeDataModel> employeeUpdatedData)
        {
            var emp = await _employeeDbContext.EmployeeData.FindAsync(empId);
            if (emp != null)
            {
                employeeUpdatedData.ApplyTo(emp);
                await _employeeDbContext.SaveChangesAsync();
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{empId}")]
        public async Task<IActionResult> DeleteEmployeeData([FromRoute]string empId)
        {
           var emp = _employeeDbContext.EmployeeData.Where(x => x.EmpId.Equals(empId)).FirstOrDefault();
            if (emp != null)
            {
                _employeeDbContext.EmployeeData.Remove(emp);
                await _employeeDbContext.SaveChangesAsync();

                return Ok();
            }
            return BadRequest();
        }
    }
}
