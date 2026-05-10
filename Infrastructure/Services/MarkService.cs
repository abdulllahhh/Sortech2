using Application.DTO.Enrollment;
using Application.DTO.Mark;
using Application.DTO.StudentReport;
using Application.Exeptions;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Domain.Models;
using Infrastructure.Data;


namespace Infrastructure.Services
{
    public class MarkService : IMarkService
    {
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<Class> _classRepository;
        private readonly IRepository<Enrollment> _enrolmentRepository;
        private readonly IRepository<Mark> _markRepository;

        public MarkService(
            IRepository<Student> studentRepository,
            IRepository<Class> classRepository,
            IRepository<Enrollment> enrolmentRepository,
            IRepository<Mark> markRepository)
        {
            _studentRepository = studentRepository;
            _classRepository = classRepository;
            _enrolmentRepository = enrolmentRepository;
            _markRepository = markRepository;
        }

        public async Task<EnrolmentResponse> EnrollStudentAsync(EnrollStudentRequest request)
        {
            var student = _studentRepository.GetByID(request.StudentId);
            if (student == null) throw new NotFoundException("Student not found.");

            var @class = _classRepository.GetByID(request.ClassId);
            if (@class == null) throw new NotFoundException("Class not found.");

            // Prevent duplicate enrollments
            var existing = _enrolmentRepository.Get(e => e.StudentId == request.StudentId && e.ClassId == request.ClassId).FirstOrDefault();
            if (existing != null) throw new Exception("Student is already enrolled in this class.");

            var enrolment = new Enrollment
            {
                Id = InMemoryDataStore.GetNextEnrolmentId(),
                StudentId = request.StudentId,
                ClassId = request.ClassId,
                EnrollmentDate = DateTime.UtcNow
            };

            _enrolmentRepository.Create(enrolment);

            return new EnrolmentResponse
            {
                Id = enrolment.Id,
                StudentId = enrolment.StudentId,
                ClassId = enrolment.ClassId,
                EnrollmentDate = enrolment.EnrollmentDate
            };
        }

        public async Task<MarkResponse> RecordMarkAsync(RecordMarkRequest request)
        {
            var student = _studentRepository.GetByID(request.StudentId);
            if (student == null) throw new NotFoundException("Student not found.");

            var @class = _classRepository.GetByID(request.ClassId);
            if (@class == null) throw new NotFoundException("Class not found.");

            // Check if student is enrolled
            var enrolment = _enrolmentRepository.Get(e => e.StudentId == request.StudentId && e.ClassId == request.ClassId).FirstOrDefault();
            if (enrolment == null) throw new Exception("Student is not enrolled in this class.");

            var markduplicate = _markRepository.Get(e => e.StudentId == request.StudentId && e.ClassId == request.ClassId).FirstOrDefault();
            if (markduplicate != null) throw new Exception("Student Has mark for this class");

            var mark = new Mark
            {
                Id = InMemoryDataStore.GetNextMarkId(),
                StudentId = request.StudentId,
                ClassId = request.ClassId,
                ExamMark = request.ExamMark,
                AssignmentMark = request.AssignmentMark
            };

            _markRepository.Create(mark);

            return new MarkResponse
            {
                Id = mark.Id,
                StudentId = mark.StudentId,
                ClassId = mark.ClassId,
                ExamMark = mark.ExamMark,
                AssignmentMark = mark.AssignmentMark,
                TotalMark = mark.TotalMark
            };
        }

        public async Task<StudentReportResponse> GetStudentReportAsync(int studentId)
        {
            var student = _studentRepository.GetByID(studentId);
            if (student == null) throw new NotFoundException("Student not found.");

            var enrolments = _enrolmentRepository.Get(e => e.StudentId == studentId);
            var marks = _markRepository.Get(m => m.StudentId == studentId);

            var report = new StudentReportResponse
            {
                StudentId = student.Id,
                StudentName = $"{student.FirstName} {student.LastName}",
                EnrolledClasses = enrolments.Select(e =>
                {
                    var m = marks.FirstOrDefault(mark => mark.ClassId == e.ClassId);
                    var @class = _classRepository.GetByID(e.ClassId);
                    return new ClassMarkDetail
                    {
                        ClassId = e.ClassId,
                        ClassName = @class?.Name ?? "Unknown",
                        ExamMark = m?.ExamMark ?? 0,
                        AssignmentMark = m?.AssignmentMark ?? 0,
                        TotalMark = m?.TotalMark ?? 0
                    };
                }).ToList()
            };

            if (report.EnrolledClasses.Any())
            {
                report.OverallAverageMark = report.EnrolledClasses.Average(c => c.TotalMark);
            }

            return report;
        }

        //public Task<IEnumerable<StudentReportResponse>> GetAllStudentReportAsync()
        //{
        //    var enrolments = _enrolmentRepository.GetAll().GroupBy(s => s.StudentId);
        //    return enrolments.Select(g => new StudentReportResponse
        //    {
        //        EnrolledClasses = _classRepository.GetAll().Where(cs => cs.Id == g.Key.ClassId).ToList()
        //    });
        //    //var marks = _markRepository.Get(m => m.StudentId == studentId);
        //}
    }
}

