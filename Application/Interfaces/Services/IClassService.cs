using Application.DTO.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IClassService
    {
        Task<ClassResponse> CreateClassAsync(CreateClassRequest request);
        Task<IEnumerable<ClassResponse>> GetAllClassesAsync(int? page = null, int? pageSize = null, string? name = null, string? teacher = null);
        Task DeleteClassAsync(int id);
        Task<decimal> GetAverageMarksAsync(int classId);
    }
}
