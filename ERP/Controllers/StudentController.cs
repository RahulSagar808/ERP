using ERP.ApplicationCore.Models;
using ERP.ApplicationCore.Services;
using ERP.Domain.Entities;
using ERP.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ERP.Controllers
{
    public class StudentController : BaseController
    {
        private readonly StudentService _studentService;
        private readonly ApplicationDbContext _context;

        public StudentController(
            StudentService studentService,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context) : base(userManager)
        {
            _studentService = studentService;
            _context = context;
        }

        // INDEX
        public async Task<IActionResult> Index()
        {
            var students = await _studentService.GetAllStudentsAsync();

            var studentList = students?.Select(student => new StudentListModel
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                DateOfBirth = student.DateOfBirth,
                PhoneNumber = student.PhoneNumber,
                Gender = student.Gender,
                Status = student.Status,
               // SchoolName = student.SchoolName,
            }).ToList() ?? new List<StudentListModel>();

            return View(studentList);
        }

       // DETAILS
        //public async Task<IActionResult> StudentDetails(string id)
        //{
        //    var StudentDetails = new StudentModel();
        //    var StudentService = await _studentService.GetStudentByIdAsync(id);
        //    StudentDetails.Id = StudentService.Id;
        //    StudentDetails.FirstName = StudentService.FirstName;
        //    StudentDetails.LastName = StudentService.LastName;
        //    StudentDetails.Email = StudentService.Email;
        //    StudentDetails.DateOfBirth = StudentService.DateOfBirth;
        //    StudentDetails.PhoneNumber = StudentService.PhoneNumber;
        //    StudentDetails.Gender = StudentService.Gender;
        //    StudentDetails.Status = StudentService.Status;
        //   // StudentDetails.SchoolName = studentListResult.SchoolName;


        //    if (StudentDetails == null)
              //{
        //        return NotFound();
               //}
        //    return View(StudentDetails);
        //}

        //  CREATE
        [HttpGet]
        public IActionResult Create()
        {
            var model = new StudentModel
            {
                DropdownListItems = _context.Schools.Select(s => new SelectListItem
                {
                    Value = s.Id,
                    Text = s.SchoolName
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentModel model)
        {
            var student = new Student
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                DateOfBirth = model.DateOfBirth,
                PhoneNumber = model.PhoneNumber,
                Gender = model.Gender,
                Address = model.Address,
                SchoolId = model.SchoolId,
                CreatedBy = CurrentUserId
            };

            await _studentService.AddStudentAsync(student);

            return RedirectToAction(nameof(Index));
        }

        //EDIT
        [HttpGet]
        public async Task<IActionResult> StudentEdit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();
            
            var student = await _context.Students.FindAsync(id);

            if (student == null)
                return NotFound();

            var model = new StudentModel
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                DateOfBirth = student.DateOfBirth,
                Gender = student.Gender,
                Address = student.Address,
              //  SchoolName = student.SchoolName,
               
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StudentEdit(string id, StudentModel model)
        {

            var student = await _context.Students.FindAsync(id);

            if (student == null)
                return NotFound();

            student.FirstName = model.FirstName;
            student.LastName = model.LastName;
            student.Email = model.Email;
            student.PhoneNumber = model.PhoneNumber;
            student.DateOfBirth = model.DateOfBirth;
            student.Gender = model.Gender;
            student.Address = model.Address;
           // student.SchoolName = model.SchoolName;

            _context.Update(student);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> StudentDelete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
                return NotFound();

            _context.Students.Remove(student);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                TempData["SuccessMessage"] = "Student deleted successfully.";
                return RedirectToAction("GetStudentList");
            }

            TempData["ErrorMessage"] = "Failed to delete the student.";
            return RedirectToAction("GetStudentList");
        }

    }
}
