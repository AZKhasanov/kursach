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
    public partial class EmployeeWindow : Window
    {
        private int? _employeeId;
        private ARMDataContext _dataContext = new();
        private CollectionViewSource departmentsViewSource;
        private CollectionViewSource typesViewSource;

        public EmployeeWindow(int? employeeId)
        {
            InitializeComponent();

            _employeeId = employeeId;
            departmentsViewSource = (CollectionViewSource) FindResource(nameof(departmentsViewSource));
            typesViewSource = (CollectionViewSource) FindResource(nameof(typesViewSource));
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            var employee = (Employee) DataContext;
            _dataContext.Employees.Attach(employee);
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
            if (_employeeId == null)
                DataContext = new Employee();
            else
                DataContext = _dataContext.Employees.SingleOrDefault(g => g.Id == _employeeId);

            _dataContext.Departments.Load();
            _dataContext.EmployeeTypes.Load();

            departmentsViewSource.Source = _dataContext.Departments.ToList();
            typesViewSource.Source = _dataContext.EmployeeTypes.ToList();
        }
    }
}
