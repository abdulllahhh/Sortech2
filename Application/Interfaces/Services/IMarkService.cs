using Application.DTO.Enrollment;
using Application.DTO.Mark;
using Application.DTO.StudentReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IMarkService
    {
        Task<EnrolmentResponse> EnrollStudentAsync(EnrollStudentRequest request);
        Task<MarkResponse> RecordMarkAsync(RecordMarkRequest request);
        Task<StudentReportResponse> GetStudentReportAsync(int studentId);
    }
}
