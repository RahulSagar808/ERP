using ERP.ApplicationCore.Models;
using ERP.ApplicationCore.Services;
using ERP.Domain.Entities;
using ERP.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP.Controllers
{
    public class EmployeeController : BaseController
    {
        private readonly EmployeeService _employeeService;
        private readonly ApplicationDbContext _context;

        public EmployeeController(
            EmployeeService employeeService,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context) : base(userManager)
        {
            _employeeService = employeeService;
            _context = context;
        }

        // LIST 
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();

            var employeeList = employees.Select(emp => new EmployeeModel
            {
                Id = emp.Id,
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                Email = emp.Email,
                PhoneNumber = emp.PhoneNumber,
                Department = emp.Department,
                Designation = emp.Designation,
                DateOfJoining = emp.DateOfJoining,
                Salary = emp.Salary,
                Status = emp.Status
            }).ToList();

            return View(employeeList);
        }

        // DETAILS
        [HttpGet]
        public async Task<IActionResult> EmployeeDetails(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var employee = await _employeeService.GetEmployeeByIdAsync(id);

            if (employee == null)
                return NotFound();

            var model = new EmployeeModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Department = employee.Department,
                Designation = employee.Designation,
                DateOfJoining = employee.DateOfJoining,
                Salary = employee.Salary,
                Status = employee.Status
            };

            return View(model);
        }

        //  CREATE 
        [HttpGet]
        public IActionResult EmployeeCreate()
        {
            return View(new EmployeeModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmployeeCreate(EmployeeModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var employee = new Employee
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Department = model.Department,
                Designation = model.Designation,
                DateOfJoining = model.DateOfJoining,
                Salary = model.Salary,
                Status = model.Status,
                CreatedBy = CurrentUserId
            };

            await _employeeService.AddEmployeeAsync(employee);

            TempData["SuccessMessage"] = "Employee created successfully.";
            return RedirectToAction(nameof(Index));
        }

        // EDIT 
        [HttpGet]
        public async Task<IActionResult> EmployeeEdit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
                return NotFound();

            var model = new EmployeeModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Department = employee.Department,
                Designation = employee.Designation,
                DateOfJoining = employee.DateOfJoining,
                Salary = employee.Salary,
                Status = employee.Status
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmployeeEdit(EmployeeModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var employee = await _context.Employees.FindAsync(model.Id);

            if (employee == null)
                return NotFound();

            employee.FirstName = model.FirstName;
            employee.LastName = model.LastName;
            employee.Email = model.Email;
            employee.PhoneNumber = model.PhoneNumber;
            employee.Department = model.Department;
            employee.Designation = model.Designation;
            employee.DateOfJoining = model.DateOfJoining;
            employee.Salary = model.Salary;
            employee.Status = model.Status;

            _context.Update(employee);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Employee updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        // DELETE 
        [HttpGet]
        public async Task<IActionResult> EmployeeDelete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
                return NotFound();

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Employee deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
