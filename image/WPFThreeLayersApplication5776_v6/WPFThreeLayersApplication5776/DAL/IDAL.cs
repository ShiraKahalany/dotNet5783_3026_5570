using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BE;

namespace DAL
{
public interface IDAL
{

    #region Student Function
    void AddStudent(Student student);
    bool RemoveStudent(int id);
    void UpdateStudent(Student student);
    Student GetStudent(int id);
    IEnumerable<Student> GetAllStudents(Func<Student, bool> predicat = null);

    #endregion

    void AddCourse(Course course);
    bool RemoveCourse(int id);
    void UpdateCourse(Course course);
    Course GetCourse(int id);
    IEnumerable<Course> GetAllCourses(Func<Course, bool> predicat = null);

    void AddStudentCourse(StudentCourseAdapter studentCourse);
    void UpdateStudentCourse(StudentCourseAdapter studentCourse);
    bool RemoveCourseFromStudent(int studentId, int courseId, int year, Semester semester);
    IEnumerable<StudentCourseAdapter> GetAllStudentCourse(Func<StudentCourseAdapter,bool> predicat=null);
}
}
