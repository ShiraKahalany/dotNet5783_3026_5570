using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BE;

namespace BL
{
public interface IBL
{        
    void AddStudent(Student student);
    bool RemoveStudent(int id);
    void UpdateStudent(Student student);
    Student GetStudent(int id);
    IEnumerable<Student> GetAllStudents(Func<Student, bool> predicat = null);

    void AddCourse(Course course);
    bool RemoveCourse(int id);
    void UpdateCourse(Course course);
    Course GetCourse(int id);
    IEnumerable<Course> GetAllCourses(Func<Course, bool> predicat = null);

    void AddCourseToStudent(int studentId, int courseId,int year, Semester semester);   
    IEnumerable<CourseInStudent> GetAllCourseOfStudent(int StudentId);
    IEnumerable<Student> GetAllStudentAtCourse(int courseId, int year, Semester semester);

        void UpdateStudentCourseGrade(int studentId, int courseId, int year, Semester semester, float? grade);
    bool RemoveCourseFromStudent(int studentId, int courseId, int year, Semester semester);

       
    
    IEnumerable<IGrouping<Campus, Student>> GetAllStudentAtCourseGroupByCampuse(
       int courseId, int year, Semester semester);
     
    
    IEnumerable<IGrouping<string,Student>> GetAllStudentAtCourseGroupByGrade(
            int courseId,int year,Semester semester);


    }
}
