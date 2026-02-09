using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.ApplicationCore.Models
{
    public class StudentModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public List<SelectListItem>? DropdownListItems { get; set; }
        public string Id { get; set; }
        public string SchoolId { get; set; }
        public bool Status { get; set; } 
    }

    public class StudentEditModel : StudentModel
    {
        public string Id { get; set; }
    }

    public class StudentListModel : StudentEditModel
    {
        public string SchoolName { get; set; }
    }
}
