using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.ApplicationCore.Models
{
    public class SchoolModel
    {
        public string? Id { get; set; }
        public string SchoolName { get; set; }
        public List<SelectListItem>? DropdownListItems { get; set; }
        public string UniversityId { get; set; }
    }
}
