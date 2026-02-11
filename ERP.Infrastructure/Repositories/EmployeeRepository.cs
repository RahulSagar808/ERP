using ERP.ApplicationCore.Interfaces;
using ERP.Domain.Entities;
using ERP.Infrastructure.Context;
using ERP.InfrastructureData.Repositories;

namespace ERP.Infrastructure.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context) : base(context) { }
    }
}
