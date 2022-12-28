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


    public class AddCourseCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public BE.Course CourseProperty { get; set; }

        //public AddCourseCommand(ref BE.Course c)
        //{

        //}

        public bool CanExecute(object parameter)
        {
            if(CourseProperty!=null)
            {
                if (CourseProperty.CourseId != 0 && CourseProperty.CourseName != "")
                    return true;
            }
            return false;
        }

        public void Execute(object parameter)
        {
            BL.FactoryBL.GetBL().AddCourse(CourseProperty);
        }
    }

    /// <summary>
    /// Interaction logic for AddCourseWindow.xaml
    /// </summary>
    public partial class AddCourseWindow : Window
    {
        BE.Course course;
        BL.IBL bl;

        public AddCourseWindow()
        {
            InitializeComponent();
            course = new BE.Course();
            bl = BL.FactoryBL.GetBL();
            this.CourseDetailsGrid.DataContext = course;

        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
              //  course.CourseId = int.Parse(this.idTextBox.Text);
              //  course.CourseName = this.nameTextBox.Text;

                bl.AddCourse(course);
                course = new BE.Course();
                this.CourseDetailsGrid.DataContext = course;

              //  this.idTextBox.ClearValue(TextBox.TextProperty);   // this.idTextBox.Text = ""
              //  this.nameTextBox.ClearValue(TextBox.TextProperty);// this.nameTextBox.Text = ""
            }
            catch (FormatException)
            {
                MessageBox.Show("check your input and try again");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }


}
