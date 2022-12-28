using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BE;

namespace BL
{
internal class BL_basic : IBL
{
        DAL.IDAL dal;

    public BL_basic()
    {
            dal = DAL.FactoryDal.GetDal();

            
                initList();
            
    }

    void initList()
    {
            Student s1 = new Student
            {
                StudentId = 123,
                IsMarried = true,
                StudentDate = DateTime.Parse("11.12.88"),
                StudentName = "ron",
                StudentCampus = Campus.Lev,
                StudentGender = Gender.male,
                ImageSource = @"images\bl_init\user_4.jpg"

            };

            try
            {
                this.AddStudent(s1);
            }
            catch (Exception)
            {
                this.UpdateStudent(s1);
            }
        
        Student s2 = new Student
        {
            StudentId = 456,
            IsMarried = true,
            StudentDate = DateTime.Parse("07.10.93"),
            StudentName = "dan",
            StudentCampus = Campus.Lev,
            StudentGender = Gender.male,
            ImageSource = @"images\bl_init\user_5.jpg"
        };
            try
            {
                this.AddStudent(s2);
            }
            catch (Exception)
            {
                this.UpdateStudent(s2);
            }

          Student s3 = new Student
        {
            StudentId = 478,
            IsMarried = false,
            StudentDate = DateTime.Parse("10.05.95"),
            StudentName = "dora",
            StudentCampus = Campus.Tal,
            StudentGender = Gender.female,
            ImageSource = @"images\bl_init\user_01.jpg"
        };
            try
            {
                this.AddStudent(s3);
            }
            catch (Exception)
            {
                this.UpdateStudent(s3);
            }


            Student s4 = new Student
            {
                StudentId = 984,
                IsMarried = true,
                StudentDate = DateTime.Parse("07.04.91"),
                StudentName = "dina",
                StudentCampus = Campus.Tal,
                StudentGender = Gender.female,
                ImageSource = @"images\bl_init\user_03.jpg"
            };
            try
            {
                this.AddStudent(s4);
            }
            catch (Exception)
            {
                this.UpdateStudent(s4);
            }



            try
            {
                this.AddCourse(new Course { CourseId = 153007, CourseName = "dot net" });
            }
            catch (Exception)
            {
                this.UpdateCourse(new Course { CourseId = 153007, CourseName = "dot net" });
            }

            try
            {
                this.AddCourse(new Course { CourseId = 153006, CourseName = "c++" });

            }
            catch (Exception)
            {

                this.UpdateCourse(new Course { CourseId = 153006, CourseName = "c++" });

            }

            try
            {
                this.AddCourse(new Course { CourseId = 153005, CourseName = "java" });

            }
            catch (Exception)
            {

                this.UpdateCourse(new Course { CourseId = 153005, CourseName = "java" });

            }
            //this.AddCourse(new Course { CourseId = 153006, CourseName = "c++" });
           // this.AddCourse(new Course { CourseId = 153005, CourseName = "java" });
    }



        public void AddStudent(Student student)
        {
            switch (student.StudentCampus)
            {
                case Campus.Lev:
                    if (student.StudentGender != Gender.male)
                        throw new Exception("campus Lev is only for men");
                    break;

                case Campus.Tal:
                    if (student.StudentGender != Gender.female)
                        throw new Exception("campus Tal is only for women");
                    break;

                default:
                    break;
            }

            dal.AddStudent(student);
        }

        public bool RemoveStudent(int id)
        {
            return dal.RemoveStudent(id);
        }

        public void UpdateStudent(Student student)
        {

            switch (student.StudentCampus)
            {
                case Campus.Lev:
                    if (student.StudentGender != Gender.male)
                        throw new Exception("campus Lev is only for men");
                    break;

                case Campus.Tal:
                    if (student.StudentGender != Gender.female)
                        throw new Exception("campus Tal is only for women");
                    break;

                default:
                    break;
            }

            dal.UpdateStudent(student);
        }

        public IEnumerable<Student> GetAllStudents(Func<Student, bool> predicat = null)
        {
            return dal.GetAllStudents(predicat);
        }

        public Student GetStudent(int id)
        {
            return dal.GetStudent(id);
        }



        public void AddCourse(Course course)
        {
             dal.AddCourse(course);
        }

        public bool RemoveCourse(int id)
        {
        
          if (dal.GetAllStudentCourse(item => item.CourseId == id).Any())
                throw new Exception(); 

            return dal.RemoveCourse(id);
        }

        public void UpdateCourse(Course course)
        {
             dal.UpdateCourse(course);
        }

        public IEnumerable<Course> GetAllCourses(Func<Course, bool> predicat=null)
        {
            return dal.GetAllCourses(predicat);
        }

        public Course GetCourse(int id)
        {
            return dal.GetCourse(id);
        }


public void AddCourseToStudent(int studentId, int courseId, int year, Semester semester)
{
    StudentCourseAdapter studentCourse = new StudentCourseAdapter
    {
        StudentId = studentId,
        CourseId = courseId,
        RegisterYear = year,
        RegisterSemester = semester
    };
    dal.AddStudentCourse(studentCourse);
}

        //public IEnumerable<Course> GetAllCourseOfStudent(int StudentId)
        //{
        //  //var sc=  dal.GetAllStudentCourse(item => item.StudentId == StudentId);
        //  //  return from item in sc
        //  //         select GetCourse(item.CourseId);

        //    return from item in dal.GetAllStudentCourse()
        //           where item.StudentId == StudentId
        //           select GetCourse(item.CourseId);
        //}

        public IEnumerable<Student> GetAllStudentAtCourse(int courseId, int year, Semester semester)
        {
            return from item in dal.GetAllStudentCourse(item => item.CourseId == courseId)
            where item.RegisterYear == year && item.RegisterSemester == semester
                   select GetStudent(item.StudentId);
        }

public void UpdateStudentCourseGrade(int studentId, int courseId, int year, Semester semester, float? grade)
{
    if (grade > 100)
        throw new Exception("The maximum grade is '100'");
    if (grade < 0)
        throw new Exception("The minimum grade is '0' (or null)");

    StudentCourseAdapter studentCourse = new StudentCourseAdapter
    {
        StudentId = studentId,
        CourseId = courseId,
        RegisterYear = year,
        RegisterSemester = semester,
        Grade = grade
    };
    dal.UpdateStudentCourse(studentCourse);
}

public bool RemoveCourseFromStudent(int studentId, int courseId, int year, Semester semester)
{
    return dal.RemoveCourseFromStudent(studentId, courseId, year, semester);
}

      
       

public IEnumerable<CourseInStudent> GetAllCourseOfStudent(int StudentId)
{
    return from item in dal.GetAllStudentCourse()
            where item.StudentId == StudentId
            select new BE.CourseInStudent
            {
                CourseId = item.CourseId,
                CourseName = GetCourse(item.CourseId).CourseName,
                Year = item.RegisterYear,
                Semester = item.RegisterSemester,
                Grade = item.Grade

            };
}


        public IEnumerable<object> ViewAllCourseOfStudent(int StudentId)
        {
            return from item in dal.GetAllStudentCourse()
                   where item.StudentId == StudentId
                   select new
                   {
                       Id = item.CourseId,
                       Name = GetCourse(item.CourseId),
                       Year = item.RegisterYear,
                       Semester = item.RegisterSemester,
                       Grade = item.Grade
                   };


            //GetCourse(item.CourseId);
        }

        public IEnumerable<IGrouping<Campus, Student>> 
            GetAllStudentAtCourseGroupByCampuse(int courseId, int year, Semester semester)
        {
            return from item in dal.GetAllStudentCourse(sc => sc.CourseId == courseId)
                   where item.RegisterYear == year && item.RegisterSemester == semester
                   let student = GetStudent(item.StudentId)
                   group student by student.StudentCampus;

        }
        public IEnumerable<IGrouping<string, Student>>
            GetAllStudentAtCourseGroupByGrade(int courseId, int year, Semester semester)
        {
            return from item in dal.GetAllStudentCourse(sc => sc.CourseId == courseId)
                   where item.RegisterYear == year && item.RegisterSemester == semester
                   group GetStudent(item.StudentId) by GetGradeMark(item.Grade);
        }


        private string GetGradeMark(float? g)
        {
            if (g == null) 
            return "no grade";
            if (g > 90)
                return "A";
            if (g > 80)
                return "B";
            if (g > 60)
                return "c";
            else
                return "D";
        }

        //public void UpdateStudentCourseGrade(int studentId, int courseId, int year, Semester semester, float? grade)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool RemoveCourseFromStudent(int studentId, int courseId, int year, Semester semester)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
