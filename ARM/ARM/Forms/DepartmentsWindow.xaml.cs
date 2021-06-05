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
    public partial class DepartmentsWindow : WindowBase
    {
        private CollectionViewSource departmentsViewSource;

        public DepartmentsWindow()
        {
            InitializeComponent();

            departmentsViewSource = (CollectionViewSource) FindResource(nameof(departmentsViewSource));
        }

        private void Window_Loaded(object sender, RoutedEventArgs args)
        {
            LoadData();
        }

        private void LoadData()
        {
            var dataCtx = new ARMDataContext();
            dataCtx.Faculties.Load();

            departmentsViewSource.Source = dataCtx.Departments.ToList();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            object[] args = { null };
            var window = ShowWindow<DepartmentWindow>(args);
            window.Closed += (sender, args) => LoadData();
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            var department = (Department)GridDepartments.CurrentItem;
            if (department != null)
            {
                object[] args = { department.Id };
                var window = ShowWindow<DepartmentWindow>(args);
                window.Closed += (sender, args) => LoadData();
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var dataCtx = new ARMDataContext();

            var department = (Department)GridDepartments.CurrentItem;
            if (department != null && MessageBox.Show(this, "Вы действительно хотите удалить запись?", "Удалить", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                dataCtx.Departments.Remove(department);
                dataCtx.SaveChanges();
                LoadData();
            }
        }
    }
}
