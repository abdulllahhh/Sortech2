using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Mark
{
    public class AverageMarksResponse
    {
        public int ClassId { get; set; }
        public decimal AvgMark { get; set; }
    }
}
