using System;
using System.Collections.Generic;

namespace ELearning.Models
{
    public partial class Grade
    {
        public Grade()
        {
            Students = new HashSet<Student>();
        }

        public long GradeId { get; set; }
        public string? GradeNameEn { get; set; }
        public string? GradeNameAr { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
