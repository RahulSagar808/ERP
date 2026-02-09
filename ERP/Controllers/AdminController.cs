using ERP.ApplicationCore.Models;
using ERP.Domain.Entities;
using ERP.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ERP.Controllers
{
    public class AdminController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager) : base(userManager)
        {
            _context = context;
        }
        public async Task<IActionResult> GetUnversityDdlListAsync()
        {
            return View(await _context.Universities.ToListAsync());
        }

        // LIST
        public async Task<IActionResult> GetUniversityList()
        {
            var universityList = new List<UniversityListModel>();

            var universityListResult = await _context.Universities.ToListAsync();
            if (universityList == null)
            {
                return View(universityList);
            }

            foreach (var university in universityListResult)
            {
                UniversityListModel model = new()
                {
                    Id = university.Id,
                    UniversityName = university.UniversityName,
                    ContactPerson = university.ContactPerson,
                    OfficialPhone = university.OfficialPhone,
                    OfficialMobile = university.OfficialMobile,
                    OfficialEmail = university.OfficialEmail,
                    OfficialLogo = university.OfficialLogo
                };
                universityList.Add(model);
            }

            return View(universityList);
        }

        // DETAILS
        public async Task<IActionResult> UniversityDetails(string id)
        {
            var UniversityDetails = new UniversityModel();
            var UniversityListResult = await _context.Universities.FirstOrDefaultAsync();
            UniversityDetails.UniversityName = UniversityListResult.UniversityName;
            UniversityDetails.ContactPerson = UniversityListResult.ContactPerson;
            UniversityDetails.OfficialPhone = UniversityListResult.OfficialPhone;
            UniversityDetails.OfficialMobile = UniversityListResult.OfficialMobile;
            UniversityDetails.OfficialEmail = UniversityListResult.OfficialEmail;
            UniversityDetails.OfficialLogo = UniversityListResult.OfficialLogo;

            if (UniversityDetails == null)
            {
                return NotFound();
            }
            return View(UniversityDetails);
        }

        // CREATE - GET
        public IActionResult Create()
        {
            return View();
        }

        // CREATE - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UniversityModel model)
        {

            if (ModelState.IsValid)
            {
                University university = new University();
                university.ContactPerson = model.ContactPerson;
                university.OfficialPhone = model.OfficialPhone;
                university.OfficialMobile = model.OfficialMobile;
                university.OfficialEmail = model.OfficialEmail;
                university.OfficialLogo = model.OfficialLogo;
                university.UniversityName = model.UniversityName;
                university.CreatedBy = CurrentUserId;
                _context.Universities.Add(university);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return RedirectToAction("GetUniversityList");
                }
                return View(model);
            }
            return View(model);
        }


        // EDIT - GET
        public async Task<IActionResult> EditUniversity(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var university = await _context.Universities
                                           .FirstOrDefaultAsync(x => x.Id == id);
            var universityModel = new UniversityModel()
            {
                UniversityName = university.UniversityName,
                ContactPerson = university.ContactPerson,
                OfficialPhone = university.OfficialPhone,
                OfficialMobile = university.OfficialMobile,
                OfficialEmail = university.OfficialEmail,
                OfficialLogo = university.OfficialLogo
            };

            if (university == null)
                return NotFound();

            return View(universityModel);
        }


        // EDIT - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUniversity(string id, UniversityModel model)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            if (!ModelState.IsValid)
                return View(model);

            var university = await _context.Universities
            .Include(u => u.Schools)
            .FirstOrDefaultAsync(u => u.Id == id);


            if (university == null)
                return NotFound();

            university.UniversityName = model.UniversityName;
            university.ContactPerson = model.ContactPerson;
            university.OfficialPhone = model.OfficialPhone;
            university.OfficialMobile = model.OfficialMobile;
            university.OfficialEmail = model.OfficialEmail;
            university.OfficialLogo = model.OfficialLogo;
            university.UpdatedOn = DateTime.Now;
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                TempData["SuccessMessage"] = "University Edit successfully.";
                return RedirectToAction("GetUniversityList");
            }
            TempData["ErrorMessage"] = "Failed to Edit the university. Please try again.";
            return View("GetUniversityList");
        }

        // DELETE - GET
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var university = await _context.Universities
                .Include(u => u.Schools)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (university == null)
                return NotFound();

            if (university.Schools != null && university.Schools.Any())
            {
                _context.Schools.RemoveRange(university.Schools);
            }

            _context.Universities.Remove(university);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                TempData["SuccessMessage"] = "University delete successfully.";
                return RedirectToAction("GetUniversityList");
            }
            TempData["ErrorMessage"] = "Failed to delete the university. Please try again.";
            return View("GetUniversityList");
        }


        //SachoolMaster
        // LIST
        public async Task<IActionResult> GetSchoolList()
        {
            var schoolList = new List<SchoolListModel>();
            var schoolListResult = await _context.Schools.Include("University").ToListAsync();
            if (schoolList == null)
            {
                return View(schoolList);

            }
            foreach (var school in schoolListResult)
            {
                SchoolListModel model = new()
                {
                    Id = school.Id,
                    SchoolName = school.SchoolName,
                    UniversityId = school.UniversityId,
                    UniversityName = school.University.UniversityName
                };
                schoolList.Add(model);
            }
            return View(schoolList);
        }

        // DETAILS
        public async Task<IActionResult> SchoolDetails(string id)
        {
            var SchoolDetails = new SchoolModel();
            var SchoolListResult = await _context.Schools.Include("University").FirstOrDefaultAsync();
            SchoolDetails.SchoolName = SchoolListResult.SchoolName;
            SchoolDetails.UniversityId = SchoolListResult.UniversityId;

            if (SchoolDetails == null)
            {
                return NotFound();
            }
            return View(SchoolDetails);
        }

        // CREATE - GET
        public IActionResult CreateSchool()
        {
            SchoolModel schoolViewModel = new SchoolModel
            {
                DropdownListItems = _context.Universities.Select(s => new SelectListItem
                {
                    Value = s.Id,
                    Text = s.UniversityName,
                }).ToList()
            };
            return View(schoolViewModel);
        }

        // CREATE - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSchool(SchoolModel model)
        {
            if (ModelState.IsValid)
            {
                School school = new School();
                school.SchoolName = model.SchoolName;
                school.UniversityId = model.UniversityId;
                school.CreatedBy = CurrentUserId;

                _context.Schools.Add(school);
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    return RedirectToAction("GetSchoolList");
                }
                return View(model);
            }
            return View(model);
        }


        // EDIT - GET
        public async Task<IActionResult> EditSchool(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var school = await _context.Schools
                                       .FirstOrDefaultAsync(s => s.Id == id);
            var schoolModel = new SchoolModel()
            {
                Id = school.Id,
                SchoolName = school.SchoolName,
                UniversityId = school.UniversityId,
                DropdownListItems = _context.Universities.Select(s => new SelectListItem
                {
                    Value = s.Id,
                    Text = s.UniversityName,
                }).ToList()
            };
            if (school == null)
                return NotFound();

            return View(schoolModel);
        }

        // EDIT - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSchool(string id, SchoolModel model)
        {
            if (string.IsNullOrEmpty(id) || id != model.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var school = await _context.Schools
                                       .FirstOrDefaultAsync(s => s.Id == id);

            if (school == null)
                return NotFound();

            school.SchoolName = model.SchoolName;
            school.UniversityId = model.UniversityId;
            school.UpdatedOn = DateTime.Now;

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                TempData["SuccessMessage"] = "School Edit successfully.";
                return RedirectToAction("GetSchoolList");
            }
            TempData["ErrorMessage"] = "Failed to Edit the School. Please try again.";
            return View("GetSchoolList");
        }


        // DELETE - GET
        [HttpGet]
        public async Task<IActionResult> DeleteSchool(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            var school = await _context.Schools
                .FirstOrDefaultAsync(s => s.Id == id);

            if (school == null)
                return NotFound();

            _context.Schools.Remove(school);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                TempData["SuccessMessage"] = "School delete successfully.";
                return RedirectToAction("GetSchoolList");
            }
            TempData["ErrorMessage"] = "Failed to delete the School. Please try again.";
            return View("GetSchoolList");
        }

        //Department
        // LIST
        public async Task<IActionResult> GetDepartmentList()
        {
            var departmentList = new List<DepartmentListModel>();

            var departmentListResult = await _context.Departments.Include("University").ToListAsync();
            if (departmentList == null)
            {
                return View(departmentList);
            }

            foreach (var department in departmentListResult)
            {
                DepartmentListModel model = new()
                {
                    Id = department.Id,
                    Name = department.Name,
                    UniversityId = department.UniversityId,
                    UniversityName = department.University.UniversityName
                };
                departmentList.Add(model);
            }
            return View(departmentList);
        }

        // DETAILS
        public async Task<IActionResult> DepartmentDetails(string id)
        {
            var departmentDetails = new DepartmentModel();
            var departmentListResult = await _context.Departments.Include("University").FirstOrDefaultAsync();
            departmentDetails.Name = departmentListResult.Name;
            departmentDetails.UniversityId = departmentListResult.UniversityId;
           
            if (departmentDetails == null)
            {
                return NotFound();
            } 
               return View(departmentDetails);
        }

        // CREATE - GET
        public IActionResult CreateDepartment()
        {
            DepartmentModel departmentViewModel = new DepartmentModel
            {
                DropdownListItems = _context.Universities.Select(s => new SelectListItem
                {
                    Value = s.Id,
                    Text = s.UniversityName,
                }).ToList()
            };
            return View(departmentViewModel);
        }

        // CREATE - POSTF
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDepartment(DepartmentModel model)
        {
            if (ModelState.IsValid)
            {
                Department department = new Department();
                department.Name = model.Name;
                department.UniversityId = model.UniversityId;
                department.CreatedBy = CurrentUserId;
                _context.Departments.Add(department);
                var result = await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Department Create successfully.";
                if (result > 0)
                {
                    return RedirectToAction("GetDepartmentList");
                }
                return View(model);
            }
            return View(model);
        }

        // EDIT - GET
        public async Task<IActionResult> EditDepartment(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var department = await _context.Departments
                                           .FirstOrDefaultAsync(d => d.Id == id);
            var departmentModel = new DepartmentModel()
            {
                Id = department.Id,
                Name = department.Name,
                UniversityId = department.UniversityId,
                DropdownListItems = _context.Universities.Select(s => new SelectListItem
                {
                    Value = s.Id,
                    Text = s.UniversityName,
                }).ToList()
            };

            if (department == null)
                return NotFound();

            return View(departmentModel);
        }

        // EDIT - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDepartment(string id, DepartmentModel model)
        {
            if (id != model.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(model);

            var department = await _context.Departments
                                           .FirstOrDefaultAsync(d => d.Id == id);

            if (department == null)
                return NotFound();
            department.Name = model.Name;
            department.UniversityId = model.UniversityId;
            department.UpdatedOn = DateTime.Now;

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                TempData["SuccessMessage"] = "Department Edit successfully.";
                return RedirectToAction("GetDepartmentList");
            }
            TempData["ErrorMessage"] = "Failed to Edit the Department. Please try again.";
            return View("GetDepartmentList");
        }

        // DELETE - GET
        [HttpGet]
        public async Task<IActionResult> DeleteDepartment(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.Id == id);
            if (department == null)
                return NotFound();

            _context.Departments.Remove(department);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                TempData["SuccessMessage"] = "Department deleted successfully.";
                return RedirectToAction("GetDepartmentList");
            }
            TempData["ErrorMessage"] = "Failed to delete the Department. Please try again.";
            return View("GetDepartmentList");

        }

    }
}

