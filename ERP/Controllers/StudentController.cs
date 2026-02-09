using ERP.ApplicationCore.Models;
using ERP.ApplicationCore.Services;
using ERP.Domain.Entities;
using ERP.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Controllers
{
    public class StudentController : BaseController
    {
        private readonly StudentService _studentService;
        private readonly ApplicationDbContext _context;

        public StudentController(StudentService studentService, UserManager<ApplicationUser> userManager, ApplicationDbContext context) : base(userManager)
        {
            _studentService = studentService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var studentList = new List<StudentListModel>();
            var students = await _studentService.GetAllStudentsAsync();

            if (students == null)
            {
                return View(studentList);
            }

            foreach (var student in students)
            {
                StudentListModel studentListModel = new StudentListModel
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Email = student.Email,
                    DateOfBirth = student.DateOfBirth,
                    PhoneNumber = student.PhoneNumber,
                };

                studentList.Add(studentListModel);
            }

            return View(studentList);
        }

        public async Task<IActionResult> Details(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null) return NotFound();
            return View(student);
        }

        [HttpGet]
        public IActionResult Create()
        {
            StudentModel studentModel = new StudentModel
            {
                DropdownListItems = _context.Schools.Select(s => new SelectListItem
                {
                    Value = s.Id,
                    Text = s.SchoolName,
                }).ToList()
            };
            return View(studentModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentModel model)
        {
            if (ModelState.IsValid)
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
                    CreatedBy = CurrentUserId,
                    SchoolId = model.SchoolId
                };

                await _studentService.AddStudentAsync(student);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}
