using Application.DTO.Student;
using Application.Exeptions;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Services
{
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<Enrollment> _enrollmentRepository;
        private readonly IRepository<Mark> _markRepository;
        private readonly IRepository<Class> _classRepository;
        public StudentService(IRepository<Student> repository, IRepository<Enrollment> enrollmentRepository, IRepository<Mark> markRepository, IRepository<Class> ClassRepo)
        {
            _studentRepository = repository;
            _enrollmentRepository = enrollmentRepository;
            _markRepository = markRepository;
            _classRepository = ClassRepo;
        }

        public async Task<StudentResponse> CreateStudentAsync(CreateStudentRequest request)
        {
            var student = new Student
            {
                Id = InMemoryDataStore.GetNextStudentId(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age
            };

            _studentRepository.Create(student);

            return new StudentResponse
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Age = student.Age
            };
        }
        public async Task<IEnumerable<StudentResponse>> GetAllStudentsAsync(
            int? pageNumber = null,
            int? pageSize = null,
            string? name = null,
            int? age = null)
        {
            var students = _studentRepository.GetAll();

            if (!string.IsNullOrWhiteSpace(name))
            {
                students = students.Where(s =>
                    (s.FirstName + " " + s.LastName)
                    .Contains(name));
            }

            if (age.HasValue)
            {
                students = students.Where(s => s.Age == age.Value);
            }

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                students = students
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }

            return students
                .Select(s => new StudentResponse
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Age = s.Age
                });
        }
        public async Task<StudentResponse> GetStudentByIdAsync(int id)
        {
            var student = _studentRepository.GetByID(id);
            var studentMarks = _markRepository.Get(s => s.StudentId == student.Id);
            var studentClasses = _enrollmentRepository.Get(s => s.StudentId == student.Id);

            if (student == null)
                throw new NotFoundException($"Student with ID {id} not found.");


            return new StudentResponse
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Age = student.Age,
                studentClasses = Aggregate(studentClasses, studentMarks)
            };
        }
        public IEnumerable<StudentClasses> Aggregate(IEnumerable<Enrollment> enrolments, IEnumerable<Mark> marks)
        {
            var NumberOfEnrollments = enrolments.Count();
            return enrolments.Select(x => new StudentClasses { ClassId = x.ClassId, ClassName = _classRepository.GetByID(x.ClassId).Name, Grade = marks.Where(x => x.Id == x.StudentId).Sum(x => x.TotalMark) / NumberOfEnrollments });
        }
        public async Task UpdateStudentAsync(int id, CreateStudentRequest request)
        {
            var student = _studentRepository.GetByID(id);
            if (student == null)
                throw new NotFoundException($"Student with ID {id} not found.");

            student.FirstName = request.FirstName;
            student.LastName = request.LastName;
            student.Age = request.Age;

            _studentRepository.Update(student);
        }

        public async Task DeleteStudentAsync(int id)
        {
            var student = _studentRepository.GetByID(id);
            if (student == null)
                throw new NotFoundException($"Student with ID {id} not found.");

            _studentRepository.Delete(student);
        }

    }
}
