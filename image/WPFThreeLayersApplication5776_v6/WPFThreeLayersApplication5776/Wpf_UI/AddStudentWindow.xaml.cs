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
public partial class AddStudentWindow : Window
{
    BE.Student student;
    BL.IBL bl;
        private List<string> errorMessages;

        public AddStudentWindow()
    {
        InitializeComponent();
           
        student = new BE.Student();
        this.DataContext = student;

        bl = BL.FactoryBL.GetBL();

        this.studentCampusComboBox.ItemsSource = Enum.GetValues(typeof(BE.Campus));
        this.studentGenderComboBox.ItemsSource = Enum.GetValues(typeof(BE.Gender));


            errorMessages = new List<string>();
    }

        private void addButton_Click(object sender, RoutedEventArgs e)
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
                    bl.AddStudent(student);
                    student = new BE.Student();
                    this.DataContext = student;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void changeImageButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog f = new Microsoft.Win32.OpenFileDialog();
            f.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            if (   f.ShowDialog()==true)
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
    }
}
