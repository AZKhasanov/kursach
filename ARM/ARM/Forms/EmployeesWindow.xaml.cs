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
using ARM.Forms.Base;
using ARM.Models;

namespace ARM.Forms
{
    /// <summary>
    /// Interaction logic for GroupsWindow.xaml
    /// </summary>
    public partial class EmployeesWindow : WindowBase
    {
        private CollectionViewSource employeesViewSource;

        public EmployeesWindow()
        {
            InitializeComponent();

            employeesViewSource = (CollectionViewSource) FindResource(nameof(employeesViewSource));
        }

        private void Window_Loaded(object sender, RoutedEventArgs args)
        {
            LoadData();
        }

        private void LoadData()
        {
            var dataCtx = new ARMDataContext();

            dataCtx.Departments.Load();
            dataCtx.EmployeeTypes.Load();

            employeesViewSource.Source = dataCtx.Employees.ToList();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            object[] args = { null };
            var window = ShowWindow<EmployeeWindow>(args);
            window.Closed += (sender, args) => LoadData();
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            var empl = (Employee)GridEmployees.CurrentItem;
            if (empl != null)
            {
                object[] args = { empl.Id };
                var window = ShowWindow<EmployeeWindow>(args);
                window.Closed += (sender, args) => LoadData();
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var empl = (Employee)GridEmployees.CurrentItem;
            if (empl != null && MessageBox.Show(this, "Вы действительно хотите удалить запись?", "Удалить", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var dataCtx = new ARMDataContext();
                dataCtx.Employees.Remove(empl);
                dataCtx.SaveChanges();
                LoadData();
            }
        }
    }
}
