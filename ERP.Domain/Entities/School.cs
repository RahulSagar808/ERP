using System.ComponentModel.DataAnnotations;

namespace ERP.Domain.Entities
{
    public class School
    {
        public School()
        {
            Id = Guid.NewGuid().ToString().ToUpper();
            CreatedOn = DateTime.Now;
            Status = true;
        }

        [Key]
        public string Id { get; set; }
        public virtual University University { get; set; }
        public string UniversityId { get; set; }
        public string SchoolName { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public bool Status { get; set; }
    }
}
