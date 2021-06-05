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
using ARM.Data;
using ARM.Forms.Base;

namespace ARM.Forms
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : WindowBase
    {
        public MainWindow()
        {
            InitializeComponent();


            var loginWin = new LoginWindow();
            if(!(loginWin.ShowDialog() ?? false))
                this.Close();
        }

        private void ButtonUsers_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow<UsersWindow>();
        }

        private void ButtonStudents_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow<StudentsWindow>();
        }

        private void ButtonGroups_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow<GroupsWindow>();
        }

        private void ButtonFaculties_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow<FacultiesWindow>();
        }

        private void ButtonSpecialities_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow<SpecialititesWindow>();
        }

        private void ButtonDepartments_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow<DepartmentsWindow>();
        }

        private void ButtonEmployees_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow<EmployeesWindow>();
        }
    }
}
