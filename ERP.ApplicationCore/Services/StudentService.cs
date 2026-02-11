using ERP.ApplicationCore.Interfaces;
using ERP.Domain.Entities;

namespace ERP.ApplicationCore.Services
{
    public class StudentService
    {
        private readonly IStudentRepository _studentRepo;

        public StudentService(IStudentRepository studentRepo)
        {
            _studentRepo = studentRepo;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
            => await _studentRepo.GetAllAsync();

        public async Task<Student?> GetStudentByIdAsync(string id)
            => await _studentRepo.GetByIdAsync(id);

        public async Task AddStudentAsync(Student student)
            => await _studentRepo.AddAsync(student);

        public async Task UpdateStudentAsync(Student student)
            => await _studentRepo.UpdateAsync(student);

        public async Task DeleteStudentAsync(string id)
            => await _studentRepo.DeleteAsync(id);

    }
}

