using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace SwaggerDemo.Models
{
    public partial class Department
    {
        [Key]
        public long DepId { get; set; }
        public string DepName { get; set; }
        public string DepDesc { get; set; }
    }
}
