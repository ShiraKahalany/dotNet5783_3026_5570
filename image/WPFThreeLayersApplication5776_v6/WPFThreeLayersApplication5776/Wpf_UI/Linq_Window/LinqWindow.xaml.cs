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
    /// Interaction logic for LinqWindow.xaml
    /// </summary>
    public partial class LinqWindow : Window
    {
        BL.IBL bl;
        public LinqWindow()
        {
            InitializeComponent();
            bl = BL.FactoryBL.GetBL();
            bl = BL.FactoryBL.GetBL();


            this.coursesComboBox.ItemsSource = bl.GetAllCourses();
            this.coursesComboBox.DisplayMemberPath = "CourseName";
            this.coursesComboBox.SelectedValuePath = "CourseId";

            this.registerSemesterComboBox.ItemsSource = Enum.GetValues(typeof(BE.Semester));
            this.registerSemesterComboBox.SelectedIndex = 0;
        }

        private int GetSelectedCoursetId()
        {
            object result = this.coursesComboBox.SelectedValue;

            if (result == null)
                throw new Exception("must select Course First");
            return (int)result;
        }


        private void AllStudent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int CourseId = GetSelectedCoursetId();

                int RegisterYear = registerYearComboBox.SelectedIndex + 5774;
                BE.Semester RegisterSemester = (BE.Semester)registerSemesterComboBox.SelectedItem;

                ShowAllStudentUserControl uc = new ShowAllStudentUserControl();
                uc.Source = bl.GetAllStudentAtCourse
                    (CourseId ,RegisterYear,RegisterSemester);

                this.page.Content = uc;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GroupByGradeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int CourseId = GetSelectedCoursetId();

                int RegisterYear = registerYearComboBox.SelectedIndex + 5774;
                BE.Semester RegisterSemester = (BE.Semester)registerSemesterComboBox.SelectedItem;

                GroupByGradeUserControl uc = new GroupByGradeUserControl();
                uc.Source =  bl.GetAllStudentAtCourseGroupByGrade
                    (CourseId, RegisterYear, RegisterSemester);

                this.page.Content = uc;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }  
        }

        private void GroupByCampusButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int CourseId = GetSelectedCoursetId();

                int RegisterYear = registerYearComboBox.SelectedIndex + 5774;
                BE.Semester RegisterSemester = (BE.Semester)registerSemesterComboBox.SelectedItem;

                GroupByCampusUserControl uc = new GroupByCampusUserControl();
                uc.Source = bl.GetAllStudentAtCourseGroupByCampuse
                    (CourseId, RegisterYear, RegisterSemester);

                this.page.Content = uc;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
