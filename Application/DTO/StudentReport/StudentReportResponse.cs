using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.StudentReport
{
    public class StudentReportResponse
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public List<ClassMarkDetail> EnrolledClasses { get; set; } = new();
        public decimal OverallAverageMark { get; set; }
    }
}
