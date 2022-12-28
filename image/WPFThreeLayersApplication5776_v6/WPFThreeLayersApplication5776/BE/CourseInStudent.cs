using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
public class CourseInStudent
{
       
    public int CourseId { get; set; }
    public string CourseName { get; set; }
    public int Year { get; set; }
       // [System.ComponentModel.DisplayName("סמסטר")]
        public BE.Semester Semester { get; set; }

        //[System.ComponentModel.Browsable(false)]
        public float? Grade { get; set; }


        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
