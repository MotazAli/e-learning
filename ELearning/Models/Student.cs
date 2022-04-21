using System;
using System.Collections.Generic;

namespace ELearning.Models
{
    public partial class Student
    {
        public long StudentId { get; set; }
        public string? StudentNameEn { get; set; }
        public string? StudentNameAr { get; set; }
        public DateTime? Birthdate { get; set; }
        public long? GradeId { get; set; }

        public virtual Grade? Grade { get; set; }
    }
}
