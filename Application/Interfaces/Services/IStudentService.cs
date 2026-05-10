using Application.DTO.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IStudentService
    {
        Task<StudentResponse> CreateStudentAsync(CreateStudentRequest request);
        Task<IEnumerable<StudentResponse>> GetAllStudentsAsync(int? pageNumber = null, int? pagesize = null, string name = null, int? age = null);
        Task<StudentResponse> GetStudentByIdAsync(int id);
        Task UpdateStudentAsync(int id, CreateStudentRequest request);
        Task DeleteStudentAsync(int id);
    }
}
