using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BE
{
public class Course
{
    public int CourseId { get; set; }
    public string CourseName { get; set; }

        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
