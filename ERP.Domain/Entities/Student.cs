namespace ERP.Domain.Entities
{
    public class Student
    {
        public Student()
        {
            Id = Guid.NewGuid().ToString().ToUpper();
            CreatedOn = DateTime.Now;
            Status = true;
        }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public bool Status { get; set; }
        public string SchoolId { get; set; }
        public virtual School School { get; set; }
    }
}

