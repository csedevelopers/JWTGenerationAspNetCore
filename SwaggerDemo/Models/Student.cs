using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace SwaggerDemo.Models
{
    public partial class Student
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? DoB { get; set; }
        public int? Flag { get; set; }
    }
}
