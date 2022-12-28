using BE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL
{
    class Dal_XML : IDAL
    {
        XElement studentRoot;
        string studentPath = @"studentXml.xml";


        XElement courseRoot;
        string coursePath = @"courseXml.xml";


        XElement studentCourseRoot;
        string studentCoursePath = @"studentCoursetXml.xml";

        public Dal_XML()
        {
            if (!File.Exists(studentPath))
                CreateFiles();
            else
                LoadData();
        }

        private void CreateFiles()
        {
            studentRoot = new XElement("students");
            studentRoot.Save(studentPath);

            courseRoot = new XElement("courses");
            courseRoot.Save(coursePath);

            studentCourseRoot = new XElement("studentsCourses");
            studentCourseRoot.Save(studentCoursePath);

        }

        private void LoadData()
        {
            try
            {
                studentRoot = XElement.Load(studentPath);
                courseRoot = XElement.Load(coursePath);
                studentCourseRoot = XElement.Load(studentCoursePath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }




        XElement ConvertStudent(BE.Student student)
        {
            XElement studentElement = new XElement("student");

            foreach (PropertyInfo item in typeof(BE.Student).GetProperties())
                studentElement.Add
                    (
                    new XElement(item.Name, item.GetValue(student, null).ToString())
                    );

            return studentElement;
        }
        BE.Student ConvertStudent(XElement element)
        {
            Student student = new Student();
               
            foreach (PropertyInfo item in typeof(BE.Student).GetProperties())
            {
                TypeConverter typeConverter = TypeDescriptor.GetConverter(item.PropertyType);
                object convertValue = typeConverter.ConvertFromString(element.Element(item.Name).Value);

                if(item.CanWrite)
                item.SetValue(student, convertValue);
            }
                   
            return student;
        }

        XElement ConvertCourse(BE.Course course)
        {
            XElement courseElement = new XElement("course");

            foreach (PropertyInfo item in typeof(BE.Course).GetProperties())
                courseElement.Add
                    (
                    new XElement(item.Name, item.GetValue(course, null).ToString())
                    );

            return courseElement;
        }
        BE.Course ConvertCourse(XElement element)
        {
            Course course = new Course();

            foreach (PropertyInfo item in typeof(BE.Course).GetProperties())
            {
                TypeConverter typeConverter = TypeDescriptor.GetConverter(item.PropertyType);
                object convertValue = typeConverter.ConvertFromString(element.Element(item.Name).Value);

                if (item.CanWrite)
                    item.SetValue(course, convertValue);
            }

            return course;
        }

        XElement ConvertStudentCourse(BE.StudentCourseAdapter studentCourse)
        {
            XElement studentCourseElement = new XElement("studentCourse");

            foreach (PropertyInfo item in typeof(BE.StudentCourseAdapter).GetProperties())
                studentCourseElement.Add
                    (
                    new XElement(item.Name, item.GetValue(studentCourse, null).ToString())
                    );

            return studentCourseElement;
        }
        BE.StudentCourseAdapter ConvertStudentCourse(XElement element)
        {
            StudentCourseAdapter studentCourse = new StudentCourseAdapter();

            foreach (PropertyInfo item in typeof(BE.StudentCourseAdapter).GetProperties())
            {
                TypeConverter typeConverter = TypeDescriptor.GetConverter(item.PropertyType);
                object convertValue = typeConverter.ConvertFromString(element.Element(item.Name).Value);

                if (item.CanWrite)
                    item.SetValue(studentCourse, convertValue);
            }

            return studentCourse;
        }




        #region Student Function
        public void AddStudent(Student student)
        {
            Student stud = GetStudent(student.StudentId);
            if (stud != null)
                throw new Exception("Student with the same id already exists...");
           
            student.ImageSource = CopyFiles(student.ImageSource, "pasport_" + student.StudentId);

            studentRoot.Add(ConvertStudent(student));

            studentRoot.Save(studentPath);

        }
        private string CopyFiles(string sourcePath, string destinationName)
        {
            try
            {
                int postfixIndex = sourcePath.LastIndexOf('.');
                string postfix = sourcePath.Substring(postfixIndex);
                destinationName += postfix;

                string destinationPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string destinationFullName = @"images\passport\" + destinationName;

                System.IO.File.Copy(sourcePath, destinationPath + "\\" + destinationFullName, true);
                return destinationFullName;
            }
            catch (Exception ex)
            {

                return @"images\passport\empty_image.jpg";
            }

        }
       
        public bool RemoveStudent(int id)
        {
            XElement toRemove = (from item in studentRoot.Elements()
                                 where int.Parse(item.Element("StudentId").Value) == id
                                 select item).FirstOrDefault();

            if (toRemove == null)
                throw new Exception("Student with the same id not found...");

            toRemove.Remove();

            studentRoot.Save(studentPath);
            return true;
        }

        public void UpdateStudent(Student student)
        {
            //    int index = studentList.FindIndex(s => s.StudentId == student.StudentId);
            //    if (index == -1)
            //        throw new Exception("Student with the same id not found...");

            //    studentList[index] = student;


            XElement toUpdate = (from item in studentRoot.Elements()
                                 where int.Parse(item.Element("StudentId").Value) == student.StudentId
                                 select item).FirstOrDefault();

            if (toUpdate == null)
                throw new Exception("Student with the same id not found...");

            foreach (PropertyInfo item in typeof(BE.Student).GetProperties())
                toUpdate.Element(item.Name).SetValue(item.GetValue(student).ToString());

            studentRoot.Save(studentPath);
        }

        public Student GetStudent(int id)
        {
            XElement stu = null;

            try
            {
                stu = (from item in studentRoot.Elements()
                       where int.Parse(item.Element("StudentId").Value) == id
                       select item).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
                         
            if(stu==null)
                return null;

            return ConvertStudent(stu);
        }

        public IEnumerable<Student> GetAllStudents(Func<Student, bool> predicat = null)
        {
            
            if (predicat == null)
            {
                return from item in studentRoot.Elements()
                       select ConvertStudent(item);
            }

            return from item in studentRoot.Elements()
                   let s = ConvertStudent(item)
                   where predicat(s)
                   select s;
        }
        #endregion


        #region Course Function
        public void AddCourse(Course course)
        {
            Course c = GetCourse(course.CourseId);
            if (c != null)
                throw new Exception("Course with the same id already exists...");
            
            
            courseRoot.Add(ConvertCourse(course));
            courseRoot.Save(coursePath);
        }

        public bool RemoveCourse(int id)
        {
            XElement toRemove = (from item in courseRoot.Elements()
                                 where int.Parse(item.Element("CourseId").Value) == id
                                 select item).FirstOrDefault();

            if (toRemove == null)
                throw new Exception("Course with the same id not found...");

            toRemove.Remove();
            courseRoot.Save(coursePath);

            return true;
        }

        public void UpdateCourse(Course course)
        {

            XElement toUpdate = (from item in courseRoot.Elements()                            
                                 where int.Parse(item.Element("CourseId").Value) == course.CourseId
                                 select item).FirstOrDefault();

            if(toUpdate == null)
                throw new Exception("Course with the same id not found...");

            foreach (PropertyInfo item in typeof(BE.Course).GetProperties())
                toUpdate.Element(item.Name).SetValue(item.GetValue(course));

            courseRoot.Save(coursePath);

        }

        public Course GetCourse(int id)
        {
            XElement course = null;
            try
            {
                course = (from item in courseRoot.Elements()
                                   where int.Parse(item.Element("CourseId").Value) == id
                                   select item).FirstOrDefault();

            }
            catch (Exception)
            {
                return null;
            }

            if (course == null)
                return null;

            return ConvertCourse(course);

        }

        public IEnumerable<Course> GetAllCourses(Func<Course, bool> predicat = null)
        {
            if (predicat == null)
            {
                return from item in courseRoot.Elements()
                       select ConvertCourse(item);
            }

            return from item in courseRoot.Elements()
                   let c = ConvertCourse(item)
                   where predicat(c)
                   select c;
        }
        #endregion


        #region StudentCours Function
        public void AddStudentCourse(StudentCourseAdapter sc)
        {
            Student stud = GetStudent(sc.StudentId);
            if (stud == null)
                throw new Exception("Student with the same id not found...");

            Course course = GetCourse(sc.CourseId);
            if (course == null)
                throw new Exception("Course with the same id not found...");

            Func<StudentCourseAdapter, bool> predicat = item =>
            {
                bool b1 = item.StudentId == sc.StudentId && item.CourseId == sc.CourseId;
                bool b2 = item.RegisterYear == sc.RegisterYear && item.RegisterSemester == sc.RegisterSemester;
                return b1 && b2;
            };


            if (GetAllStudentCourse(predicat).Any())
                throw new Exception("can not Register the course in the same year and the same semester more than once...");

            studentCourseRoot.Add(ConvertStudentCourse(sc));
            studentCourseRoot.Save(studentCoursePath);
        }

        public void UpdateStudentCourse(StudentCourseAdapter sc)
        {
            Predicate<StudentCourseAdapter> predicat = item =>
            {
                bool b1 = item.StudentId == sc.StudentId && item.CourseId == sc.CourseId;
                bool b2 = item.RegisterYear == sc.RegisterYear && item.RegisterSemester == sc.RegisterSemester;
                return b1 && b2;
            };

            XElement toUpdate = ( from item in studentCourseRoot.Elements()
                                let temp = ConvertStudentCourse(item)
                                where predicat(temp)
                                select item).FirstOrDefault();
            if(toUpdate==null)
                throw new Exception("not found...");

            foreach (PropertyInfo item in typeof(BE.StudentCourseAdapter).GetProperties())
                toUpdate.Element(item.Name).SetValue(item.GetValue(sc));
                
            studentCourseRoot.Save(studentCoursePath);
        }

        public bool RemoveCourseFromStudent(int studentId, int courseId, int year, Semester semester)
        {
            Func<StudentCourseAdapter, bool> predicat = item =>
            {
                bool b1 = item.StudentId == studentId && item.CourseId == courseId;
                bool b2 = item.RegisterYear == year && item.RegisterSemester == semester;
                return b1 && b2;
            };

            XElement toRemove = (from item in studentCourseRoot.Elements()
                                 let temp = ConvertStudentCourse(item)
                                 where predicat(temp)
                                 select item).FirstOrDefault();

            if (toRemove == null)
                return false;

            toRemove.Remove();
           
            studentCourseRoot.Save(studentCoursePath);

            return true;
        }

        public IEnumerable<StudentCourseAdapter> GetAllStudentCourse(Func<StudentCourseAdapter, bool> predicat = null)
        {
            if (predicat == null)
            {
                return from item in studentCourseRoot.Elements()
                       select ConvertStudentCourse(item);
            }

            return from item in studentCourseRoot.Elements()
                   let sc = ConvertStudentCourse(item)
                   where predicat(sc)
                   select sc;
        }
        #endregion
    }
}
