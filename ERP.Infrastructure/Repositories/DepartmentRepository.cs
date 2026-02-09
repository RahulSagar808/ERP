using ERP.ApplicationCore.Interfaces;
using ERP.Domain.Entities;
using ERP.Infrastructure.Context;
using ERP.InfrastructureData.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ERP.Infrastructure.Repositories
{
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Department?> GetDepartmentWithFacultyAsync(string departmentId)
        {
            return await _context.Departments.Include(d => d.University).FirstOrDefaultAsync(d => d.Id == departmentId);
        }
    }
}
