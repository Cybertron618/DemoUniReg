using System.Collections.Generic;
using System.Threading.Tasks;
using DemoUniReg.Api.Models;

namespace DemoUniReg.Api.Infrastructures.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<StudentDetail>> GetStudentDetailsAsync();
        Task<StudentDetail?> GetStudentDetailByIdAsync(int id);
        Task CreateStudentDetailAsync(StudentDetail studentDetail);
        Task UpdateStudentDetailAsync(StudentDetail studentDetail);
        Task DeleteStudentDetailAsync(int id);
        Task<bool> StudentDetailExistsAsync(int id);

        // Add methods for StudentMIS
        Task<IEnumerable<StudentMIS>> GetStudentMISsAsync();
        Task<StudentMIS?> GetStudentMISByIdAsync(int id);
        Task CreateStudentMISAsync(StudentMIS studentMIS);
        Task UpdateStudentMISAsync(StudentMIS studentMIS);
        Task DeleteStudentMISAsync(int id);
        Task<bool> StudentMISExistsAsync(int id);
    }
}

