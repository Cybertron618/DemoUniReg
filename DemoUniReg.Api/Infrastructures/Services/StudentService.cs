using DemoUniReg.Api.Models;
using DemoUniReg.Api.Infrastructures.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoUniReg.Api.Infrastructures.Services
{
    public class StudentService(IStudentRepository studentRepository) : IStudentService
    {
        private readonly IStudentRepository _studentRepository = studentRepository;

        public async Task<IEnumerable<StudentDetail>> GetStudentDetailsAsync()
        {
            return await _studentRepository.GetStudentDetailsAsync();
        }

        public async Task<StudentDetail?> GetStudentDetailByIdAsync(int id)
        {
            return await _studentRepository.GetStudentDetailByIdAsync(id);
        }

        public async Task CreateStudentDetailAsync(StudentDetail studentDetail)
        {
            await _studentRepository.CreateStudentDetailAsync(studentDetail);
        }

        public async Task UpdateStudentDetailAsync(StudentDetail studentDetail)
        {
            await _studentRepository.UpdateStudentDetailAsync(studentDetail);
        }

        public async Task DeleteStudentDetailAsync(int id)
        {
            await _studentRepository.DeleteStudentDetailAsync(id);
        }

        public async Task<bool> StudentDetailExistsAsync(int id)
        {
            return await _studentRepository.StudentDetailExistsAsync(id);
        }

        public async Task<IEnumerable<StudentMIS>> GetStudentMISsAsync()
        {
            return await _studentRepository.GetStudentMISsAsync();
        }

        public async Task<StudentMIS?> GetStudentMISByIdAsync(int id)
        {
            return await _studentRepository.GetStudentMISByIdAsync(id);
        }

        public async Task CreateStudentMISAsync(StudentMIS studentMIS)
        {
            await _studentRepository.CreateStudentMISAsync(studentMIS);
        }

        public async Task UpdateStudentMISAsync(StudentMIS studentMIS)
        {
            await _studentRepository.UpdateStudentMISAsync(studentMIS);
        }

        public async Task DeleteStudentMISAsync(int id)
        {
            await _studentRepository.DeleteStudentMISAsync(id);
        }

        public async Task<bool> StudentMISExistsAsync(int id)
        {
            return await _studentRepository.StudentMISExistsAsync(id);
        }
    }
}
