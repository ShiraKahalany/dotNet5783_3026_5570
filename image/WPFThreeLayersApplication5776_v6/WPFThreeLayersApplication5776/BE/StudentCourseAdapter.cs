using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class StudentCourseAdapter 
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public float? Grade { get; set; }
        public int RegisterYear { get; set; }
        public Semester RegisterSemester { get; set; }

        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
