using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.ApplicationCore.Models
{
    public class DepartmentModel
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public List<SelectListItem>? DropdownListItems { get; set; }
        public string UniversityId { get; set; }
    }
}
