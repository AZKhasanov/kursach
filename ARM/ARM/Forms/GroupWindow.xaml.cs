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
    public partial class GroupWindow : Window
    {
        private int? _groupId;
        private ARMDataContext _dataContext = new();
        private CollectionViewSource employeeViewSource;
        private CollectionViewSource specialityViewSource;

        public GroupWindow(int? groupId)
        {
            InitializeComponent();

            _groupId = groupId;
            employeeViewSource = (CollectionViewSource) FindResource(nameof(employeeViewSource));
            specialityViewSource = (CollectionViewSource) FindResource(nameof(specialityViewSource));
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            var group = (Group) DataContext;
            _dataContext.Groups.Attach(group);
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
            if (_groupId == null)
            {
                DataContext = new Group()
                {
                    DateBegin = DateTime.Now
                };
            }
            else
                DataContext = _dataContext.Groups.SingleOrDefault(g => g.Id == _groupId);

            _dataContext.Faculties.Load();
            _dataContext.EmployeeTypes.Load();

            employeeViewSource.Source = _dataContext.Employees.Where(e => e.EmployeeTypeID == 1).ToList();
            specialityViewSource.Source = _dataContext.Specialities.ToList();
        }
    }
}
