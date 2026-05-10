using Application.DTO.Class;
using Application.Exeptions;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Domain.Models;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ClassService : IClassService
    {
        private readonly IRepository<Class> _classRepository;
        private readonly IRepository<Mark> _markRepository;

        public ClassService(IRepository<Class> classRepository, IRepository<Mark> markRepository)
        {
            _classRepository = classRepository;
            _markRepository = markRepository;
        }

        public async Task<ClassResponse> CreateClassAsync(CreateClassRequest request)
        {
            var @class = new Class
            {
                Id = InMemoryDataStore.GetNextClassId(),
                Name = request.Name,
                Teacher = request.Teacher
            };

            _classRepository.Create(@class);

            return new ClassResponse
            {
                Id = @class.Id,
                Name = @class.Name,
                Teacher = @class.Teacher
            };
        }

        public async Task<IEnumerable<ClassResponse>> GetAllClassesAsync(int? page = null, int? pageSize = null, string? name = null, string? teacher = null)
        {
            var classes = _classRepository.GetAll();

            if (!string.IsNullOrEmpty(name))
            {
                classes = classes.Where(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(teacher))
            {
                classes = classes.Where(c => c.Teacher.Contains(teacher, StringComparison.OrdinalIgnoreCase));
            }

            if (page.HasValue && pageSize.HasValue)
            {
                classes = classes.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return classes.Select(c => new ClassResponse
            {
                Id = c.Id,
                Name = c.Name,
                Teacher = c.Teacher
            });
        }

        public async Task DeleteClassAsync(int id)
        {
            var @class = _classRepository.GetByID(id);
            if (@class == null) throw new NotFoundException($"Class with ID {id} not found.");
            _classRepository.Delete(@class);
        }

        public async Task<decimal> GetAverageMarksAsync(int classId)
        {
            var @class = _classRepository.GetByID(classId);
            if (@class == null) throw new NotFoundException($"Class with ID {classId} not found.");

            var marks = _markRepository.Get(m => m.ClassId == classId);
            if (!marks.Any()) return 0;

            return marks.Average(m => m.TotalMark);
        }
    }
}

