using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Class : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Teacher { get; set; }
        public string Description { get; set; }
    }
}
