using ERP.ApplicationCore.Interfaces;
using ERP.Domain.Entities;
using ERP.Infrastructure.Context;
using ERP.InfrastructureData.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ERP.Infrastructure.Repositories
{
    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Student?> GetByIdAsync(int id)
            => await _context.Students.FindAsync(id);

        public async Task<IEnumerable<Student>> GetAllAsync()
            => await _context.Students.ToListAsync();

        public async Task AddAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<Student>> GetAsync(Expression<Func<Student, bool>>? filter = null, Func<IQueryable<Student>, IOrderedQueryable<Student>>? orderBy = null, string includeProperties = "", int? page = null, int? pageSize = null)
        {
            throw new NotImplementedException();
        }
    }
}
