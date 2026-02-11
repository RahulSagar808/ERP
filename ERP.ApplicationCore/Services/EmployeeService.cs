using ERP.ApplicationCore.Interfaces;
using ERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.ApplicationCore.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _employeeRepo;

        public EmployeeService(IEmployeeRepository employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
            => await _employeeRepo.GetAllAsync();

        public async Task<Employee?> GetEmployeeByIdAsync(string id)
            => await _employeeRepo.GetByIdAsync(id);

        public async Task AddEmployeeAsync(Employee employee)
            => await _employeeRepo.AddAsync(employee);

        public async Task UpdateEmployeeAsync(Employee employee)
            => await _employeeRepo.UpdateAsync(employee);

        public async Task DeleteEmployeeAsync(string id)
            => await _employeeRepo.DeleteAsync(id);
    }
}

