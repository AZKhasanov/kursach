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
    /// Interaction logic for Students.xaml
    /// </summary>
    public partial class StudentsWindow : WindowBase
    {
        private ARMDataContext _dataContext = new();
        public CollectionViewSource studentsViewSource;
        private StudentWindow _studentWindow;

        public StudentsWindow()
        {
            InitializeComponent();
            studentsViewSource = (CollectionViewSource)FindResource(nameof(studentsViewSource));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            object[] args = { null };
            var window = ShowWindow<StudentWindow>(args);
            window.Closed += (sender, args) => LoadData();
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            var student = (Student)StudentsGrid.CurrentItem;
            if (student != null)
            {
                object[] args = { student.Id };
                var window = ShowWindow<StudentWindow>(args);
                window.Closed += (sender, args) => LoadData();
            }
        }

        private void LoadData()
        {
            _dataContext.Groups.Load();
            _dataContext.Specialities.Load();
            studentsViewSource.Source = _dataContext.Students.ToList();
            StudentsGrid.UpdateLayout();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var student = (Student)StudentsGrid.CurrentItem;
            if (student != null && MessageBox.Show(this, "Вы действительно хотите удалить запись?", "Удалить", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _dataContext.Students.Remove(student);
                _dataContext.SaveChanges();
                LoadData();
            }
        }

    }
}
