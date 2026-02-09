namespace ERP.Domain.Entities
{
    public class University
    {
        public University()
        {
            Id = Guid.NewGuid().ToString().ToUpper();
            CreatedOn = DateTime.UtcNow;
            Status = true;
        }

        public string Id { get; set; }
        public string UniversityName { get; set; }
        public string ContactPerson { get; set; }
        public string OfficialPhone { get; set; }
        public string OfficialMobile { get; set; }
        public string OfficialEmail { get; set; }
        public string OfficialLogo { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public bool Status { get; set; }
        public List<School> Schools { get; set; }
        public List<Department> Departments { get; set; }
    }
}
