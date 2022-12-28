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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
public MainWindow()
{
    InitializeComponent();
    Uri dictUri = new Uri(@"/res/languages/AppStrings_HE.xaml", UriKind.Relative);
    ResourceDictionary resourceDict = Application.LoadComponent(dictUri) as ResourceDictionary;
    Application.Current.Resources.MergedDictionaries.Clear();
    Application.Current.Resources.MergedDictionaries.Add(resourceDict);
}

        private void addStudentButton_Click(object sender, RoutedEventArgs e)
        {
            Window addStudentWindow = new AddStudentWindow();
            addStudentWindow.Show();
        }

        private void addCourseButton_Click(object sender, RoutedEventArgs e)
        {
            new AddCourseWindow().Show();
        }

        private void addCourseTOstudentButton_Click(object sender, RoutedEventArgs e)
        {
            new AddCourseToStudentWindow().ShowDialog();
        }

        private void linqButton_Click(object sender, RoutedEventArgs e)
        {
            new LinqWindow().ShowDialog();
        }

        private void updateStudentButton_Click(object sender, RoutedEventArgs e)
        {
            new UpdateStudentWindow().Show();
        }
    }
}
