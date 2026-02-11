using ERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.ApplicationCore.Interfaces
{
    public interface IEmployeeRepository : IRepositoryBase<Employee>
    {
        //Task DeleteAsync(string id);
        //Task<Employee?> GetByIdAsync(string id);
    }
}
