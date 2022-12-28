using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Wpf_UI
{
    /// <summary>
    /// Interaction logic for AddCourseToStudentWindow.xaml
    /// </summary>
public partial class AddCourseToStudentWindow : Window
{
    BL.IBL bl;
    BE.CourseInStudent courseInStudent;

    public AddCourseToStudentWindow()
    {
        InitializeComponent();
        bl = BL.FactoryBL.GetBL();

        this.coursesComboBox.ItemsSource = bl.GetAllCourses();
        this.coursesComboBox.DisplayMemberPath = "CourseName";
        this.coursesComboBox.SelectedValuePath = "CourseId";

        this.studentsComboBox.ItemsSource = bl.GetAllStudents();
        this.studentsComboBox.DisplayMemberPath = "StudentName";
        this.studentsComboBox.SelectedValuePath = "StudentId";

        this.registerSemesterComboBox.ItemsSource = Enum.GetValues(typeof(BE.Semester));
        this.registerSemesterComboBox.SelectedIndex = 0;
    }


private int GetSelectedStudentId()
{
    object result = this.studentsComboBox.SelectedValue;

    if (result == null)
        throw new Exception("must select Student First");
    return (int)result;
}

private int GetSelectedCoursetId()
{
    object result = this.coursesComboBox.SelectedValue;

    if (result == null)
        throw new Exception("must select Course First");
    return (int)result;
}

        private void refreshDataGrid(int studentId)
        {
            try
            {
                StudentCoursesDataGrid.ItemsSource = bl.GetAllCourseOfStudent(studentId);
            }
            catch
            {


            }

        }

private void updateGradeButton_Click(object sender, RoutedEventArgs e)
{
    try
    {
        if (courseInStudent != null)
        {
            courseInStudent.Grade = this.gradeNumUpDown.Value;
            int StudentId = GetSelectedStudentId();

            bl.UpdateStudentCourseGrade(
                StudentId,
                courseInStudent.CourseId,
                courseInStudent.Year,
                courseInStudent.Semester,
                courseInStudent.Grade);

            refreshDataGrid(StudentId);
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message);
    }
}

private void addCourseToStudentButton_Click(object sender, RoutedEventArgs e)
{
    try
    {
        int StudentId = GetSelectedStudentId();
        int CourseId = GetSelectedCoursetId();

        int RegisterYear = registerYearComboBox.SelectedIndex + 5774;
        BE.Semester RegisterSemester = (BE.Semester)registerSemesterComboBox.SelectedItem;

        bl.AddCourseToStudent(StudentId, CourseId, RegisterYear, RegisterSemester);

        refreshDataGrid(StudentId);
    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message);
    }
}

private void studentsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
{
    if (sender is ComboBox && ((ComboBox)sender).SelectedIndex > -1)
        this.refreshDataGrid(GetSelectedStudentId());
}

private void StudentCoursesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
{
    try
    {
        DataGrid dg = sender as DataGrid;
                if (dg.SelectedIndex > -1)
                {
                    courseInStudent = dg.SelectedItem as BE.CourseInStudent;
                    this.gradeNumUpDown.Value = courseInStudent.Grade;
                }
                else
                {
                    courseInStudent = null;
                }
    }
    catch
    {

    }
}
}
}
