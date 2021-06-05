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
        public CollectionViewSource studentsViewSource;

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
            var dataCtx = new ARMDataContext();
            dataCtx.Groups.Load();
            dataCtx.Specialities.Load();
            studentsViewSource.Source = dataCtx.Students.ToList();
            StudentsGrid.UpdateLayout();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var student = (Student)StudentsGrid.CurrentItem;
            if (student != null && MessageBox.Show(this, "Вы действительно хотите удалить запись?", "Удалить", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var dataCtx = new ARMDataContext();
                dataCtx.Students.Remove(student);
                dataCtx.SaveChanges();
                LoadData();
            }
        }

        private void ButtonChangeState_Click(object sender, RoutedEventArgs e)
        {
            var student = (Student)StudentsGrid.CurrentItem;
            if (student != null)
            {
                object[] args = { student.Id };
                var window = ShowWindow<StudentsActionsWindow>(args);
                window.Closed += (sender, args) => LoadData();
            }
        }

        private void ButtonPayments_Click(object sender, RoutedEventArgs e)
        {
            var student = (Student)StudentsGrid.CurrentItem;
            if (student != null)
            {
                object[] args = { student.Id };
                var window = ShowWindow<StudentsPaymentsWindow>(args);
                window.Closed += (sender, args) => LoadData();
            }
        }

        private void ButtonReport_Click(object sender, RoutedEventArgs e)
        {
            var student = (Student)StudentsGrid.CurrentItem;
            if (student != null)
            {
                object[] args = { student.Id };
                var window = ShowWindow<StudentActionsReportWindow>(args);
                window.Closed += (sender, args) => LoadData();
            }
        }
    }
}
