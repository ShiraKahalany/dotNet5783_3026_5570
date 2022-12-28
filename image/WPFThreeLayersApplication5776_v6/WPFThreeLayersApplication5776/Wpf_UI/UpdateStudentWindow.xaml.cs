using BE;
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
    /// Interaction logic for AddStudentWindow.xaml
    /// </summary>
    public partial class UpdateStudentWindow : Window
    {
        BE.Student studentToUpdate;
        BL.IBL bl;
        private List<string> errorMessages;

        public UpdateStudentWindow()
        {
            InitializeComponent();

            studentToUpdate = null;//new BE.Student();
                                   //  this.DataContext = studentToUpdate;

            bl = BL.FactoryBL.GetBL();

            this.studentCampusComboBox.ItemsSource = Enum.GetValues(typeof(BE.Campus));
            this.studentGenderComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gender));

            refreshData(); //this.studentIdComboBox.ItemsSource = bl.GetAllStudents();
            this.studentIdComboBox.DisplayMemberPath = "StudentId";
            this.studentIdComboBox.SelectedValuePath = "StudentId";
            
            errorMessages = new List<string>();
        }



        private void changeImageButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog f = new Microsoft.Win32.OpenFileDialog();
            f.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            if (f.ShowDialog() == true)
            {
                this.studentImage.Source = new BitmapImage(new Uri(f.FileName));

            }
        }


        private void validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                errorMessages.Add(e.Error.Exception.Message);
            else
                errorMessages.Remove(e.Error.Exception.Message);

            this.addButton.IsEnabled = !errorMessages.Any();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (errorMessages.Any()) //errorMessages.Count > 0 
                {
                    string err = "Exception:";
                    foreach (var item in errorMessages)
                        err += "\n" + item;

                    MessageBox.Show(err);
                    return;
                }
                else
                {
                    bl.UpdateStudent(studentToUpdate);
                    this.DataContext = studentToUpdate = null;
                    refreshData();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void studentIdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.studentIdComboBox.SelectedItem is Student)
            {
                this.studentToUpdate = ((Student)this.studentIdComboBox.SelectedItem).DeepClone();
                this.DataContext = studentToUpdate;
            }
        }

        private void deleteDataGridButton_Click(object sender, RoutedEventArgs e)
        {
            Student obj = this.StudentDataGrid.SelectedItem as Student;
            if (obj != null)
            {
                MessageBox.Show($"delete Student: \n{obj}");
            }
        }


        private void refreshData()
        {
            try
            {
                this.studentIdComboBox.ItemsSource = bl.GetAllStudents();
                this.StudentDataGrid.ItemsSource = bl.GetAllStudents();
            }
            catch
            {
                this.Close();
            }
        }
    }
}
