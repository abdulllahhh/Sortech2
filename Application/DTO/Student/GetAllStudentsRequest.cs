using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Student
{
    public class GetAllStudentsRequest
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public string? Name { get; set; }
        public int? Age { get; set; }

    }
}
