using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using ARM.Data;
using ARM.Models;

namespace ARM.Forms
{
    /// <summary>
    /// Interaction logic for GroupWindow.xaml
    /// </summary>
    public partial class FacultyWindow : Window
    {
        private int? _facultyId;
        private ARMDataContext _dataContext = new();

        public FacultyWindow(int? facultyId)
        {
            InitializeComponent();

            _facultyId = facultyId;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            var faculty = (Faculty) DataContext;
            _dataContext.Faculties.Attach(faculty);
            _dataContext.SaveChanges();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs args)
        {
            LoadData();
        }

        private void LoadData()
        {
            _dataContext.EmployeeTypes.Load();
            _dataContext.Employees.Load();

            DataContext = _facultyId == null ? new Faculty() : _dataContext.Faculties.SingleOrDefault(f => f.Id == _facultyId);
        }
    }
}
