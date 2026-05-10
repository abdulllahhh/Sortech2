using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Enrollment
{
    public class EnrollStudentRequest
    {
        public int StudentId { get; set; }
        public int ClassId { get; set; }
    }
}
