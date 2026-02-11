using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.ApplicationCore.Models
{
    public class EmployeeModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public DateTime DateOfJoining { get; set; }
        public decimal Salary { get; set; }
        public bool Status { get; set; }

    }
}
