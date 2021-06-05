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
    public partial class DepartmentWindow : Window
    {
        private int? _departmentId;
        private ARMDataContext _dataContext = new();
        private CollectionViewSource facultiesViewSource;

        public DepartmentWindow(int? departmentId)
        {
            InitializeComponent();

            _departmentId = departmentId;
            facultiesViewSource = (CollectionViewSource) FindResource(nameof(facultiesViewSource));
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            var dept = (Department) DataContext;
            _dataContext.Departments.Attach(dept);
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
            if (_departmentId == null)
                DataContext = new Department();
            else
                DataContext = _dataContext.Departments.SingleOrDefault(g => g.Id == _departmentId);

            _dataContext.Faculties.Load();

            facultiesViewSource.Source = _dataContext.Faculties.ToList();
        }
    }
}
