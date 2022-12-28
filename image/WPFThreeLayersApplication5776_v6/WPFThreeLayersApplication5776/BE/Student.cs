using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace BE
{
    [Serializable]
    public class Student //: INotifyDataErrorInfo, IDataErrorInfo
    {
        public TimeSpan StudentLifeTimeSpan { get { return DateTime.Now - StudentDate; }  }


        private int id;
        public int StudentId
        {
            get { return id; }
            set
            {
                if (value < 0)
                    throw new Exception("id Must be positive ...");
                this.id = value;
            }
        }

        //   [System.ComponentModel.DisplayName("שם הסטודנט")]

        [System.ComponentModel.DataAnnotations.Display(Name = "שם הסטודנט",ShortName ="שם" ,Description = "תאור העמודה")]
        public string StudentName { get; set; }

        [Browsable(false)]
        public string ImageSource
        {
            get { return imageSource; }
            set { imageSource = value; }
        }

        public bool IsMarried { get; set; }
        public DateTime StudentDate { get; set; }
        public Gender StudentGender { get; set; }
        public Campus StudentCampus { get; set; }

        // public string ImageSource  { get; set; }
        //images/pasport/user_8.jpg

        private string imageSource;


        public Student()
        {
            imageSource = (@"Empty Image");
            StudentDate = DateTime.Parse("01.01.1995");
          //  StudentLifeTimeSpan = TimeSpan.Parse("21:31:00");

        }
        //    public List<Course> Courses { get; set; }

        public override string ToString()
        {
            return this.ToStringProperty();
        }

        public Student GetCopy()
        {
            return (Student)this.MemberwiseClone();
        }

    }

}
