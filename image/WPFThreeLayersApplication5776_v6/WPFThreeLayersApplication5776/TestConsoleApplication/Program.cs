using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace TestConsoleApplication
{
    class Program
    {
        static BL.IBL bl = BL.FactoryBL.GetBL();
        static int[] studentId = new int[] { 1, 2, 3, 4 };
        static int[] courseId = new int[] { 11, 22, 33, 44 };
        static int[] year = new int[] { 5774, 5775, 5776 };


        static void addTest()
        {
            foreach (var item in studentId)
            {
                bl.AddStudent(
                    new Student
                    {
                        StudentId = item,
                        StudentName = "user "+item,
                        StudentCampus = item%2==0 ? Campus.Lev : Campus.Tal,
                        StudentGender = item%2==0 ? Gender.male : Gender.female                       
                    });
            }

            foreach (var item in courseId)
            {
                bl.AddCourse(
                    new Course
                    {
                        CourseId = item,
                        CourseName = "course " + item,
                    });
            }

            
            bl.AddCourseToStudent(studentId[0], courseId[0], 5774, Semester.a);
            bl.AddCourseToStudent(studentId[0], courseId[2], 5774, Semester.a);
            bl.AddCourseToStudent(studentId[1], courseId[2], 5774, Semester.b);

        }

        private static void UpdateStudentTest()
        {
            Console.WriteLine("all Students:");
            foreach (var item in bl.GetAllStudents())
            {
                Console.WriteLine(item);
            }

            Student source = bl.GetStudent(studentId[0]);

            Student toUpdate = new Student
            {
                StudentId = studentId[0],
                StudentName = "student 1",
                IsMarried = !source.IsMarried
            };

            bl.UpdateStudent(toUpdate);

            bl.RemoveStudent(studentId[2]);

            Console.WriteLine("-----------------");
            Console.WriteLine("all Students:");
            foreach (var item in bl.GetAllStudents())
            {
                Console.WriteLine(item);
            }
        }

        private static void UpdateCourseTest()
        {
            Console.WriteLine("all Courses:");
            foreach (var item in bl.GetAllCourses())
            {
                Console.WriteLine(item);
            }

          
            Course toUpdate = new Course
            {
                CourseId = courseId[2],
                CourseName = "update course name",
               
            };

            bl.UpdateCourse(toUpdate);

            bl.RemoveCourse(courseId[1]);

            Console.WriteLine("-----------------");
            Console.WriteLine("all courses:");
            foreach (var item in bl.GetAllCourses())
            {
                Console.WriteLine(item);
            }
        }

        [Serializable]
        class A
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        [Serializable]
        class B
        {
            public DateTime MyDateTime { get; set; }
            public  List<A> Alist { get; set; }
            public A MyA { get; set; }
            public string Name { get; set; }

            public B GetCopy()
            {
                return (B)MemberwiseClone();
            }
        }

       private static void printB(B b)
        {
            Console.WriteLine($"Name: {b.Name}");
            Console.WriteLine($"Date: {b.MyDateTime}");
            Console.WriteLine($"MyA.Id = {b.MyA.Id,-5} MyA.Name = {b.MyA.Name} ");
            foreach (var item in b.Alist)
            {
                Console.WriteLine($"Id = {item.Id,-5} Name = {item.Name} ");
            }

        }


        private static void testDeepCopy()
        {
            List<A> list = new List<A>
            {
                new A { Id = 1, Name = "user 1" },
                new A { Id = 2, Name = "user 2" },
                new A { Id = 3, Name = "user 3" }
            };

            B b = new B
            {
                MyDateTime = DateTime.Parse("01/12/1985"),
                Alist = list,
                MyA = new A { Id = 4, Name = "user 4" },
                Name = "temp string"
                

            };

            B b2 = b.DeepClone();
           // b2.Alist = new List<A>();
           // b2.Alist.AddRange(b.Alist);
            b2.Name += " copy";
            b2.MyDateTime = DateTime.Parse("02/11/1987");
            b2.MyA.Id++;
            b2.MyA.Name += "copy ";
            foreach (var item in b2.Alist)
            {
                item.Id++;
                item.Name += " copy";
            }

            printB(b);
            Console.WriteLine("----------");
            printB(b2);
        }

        static void Main(string[] args)
        {
            //Console.WriteLine("insert element :");
            //addTest();
            //Console.WriteLine("-----------------");
            //UpdateStudentTest();
            //Console.WriteLine("-----------------");
            //UpdateCourseTest();
            //Console.WriteLine("-----------------");
            testDeepCopy();
        }

       
    }
}
