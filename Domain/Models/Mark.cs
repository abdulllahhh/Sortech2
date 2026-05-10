using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Mark : IBaseEntity
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public decimal ExamMark { get; set; }
        public decimal AssignmentMark { get; set; }
        public decimal TotalMark => ExamMark + AssignmentMark;

    }
}
