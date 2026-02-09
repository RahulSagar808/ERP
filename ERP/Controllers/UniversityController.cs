using ERP.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Controllers
{
    public class UniversityController : Controller
    {
        public IActionResult Add()
        {
            return View();
        }

        public IActionResult GetUniversityList()
        {
            return View();
        }


        [Authorize]
        [HttpPost]
        public IActionResult Add(University university)
        {
            return View(university);
        }
    }
}
