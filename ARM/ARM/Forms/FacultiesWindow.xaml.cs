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
    public partial class FacultiesWindow : WindowBase
    {
        private CollectionViewSource facultiesViewSource;

        public FacultiesWindow()
        {
            InitializeComponent();

            facultiesViewSource = (CollectionViewSource) FindResource(nameof(facultiesViewSource));
        }

        private void Window_Loaded(object sender, RoutedEventArgs args)
        {
            LoadData();
        }

        private void LoadData()
        {
            var dataCtx = new ARMDataContext();

            dataCtx.Employees.Load();

            facultiesViewSource.Source = dataCtx.Faculties.ToList();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            object[] args = { null };
            var window = ShowWindow<FacultyWindow>(args);
            window.Closed += (sender, args) => LoadData();
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            var faculty = (Faculty)GridFaculties.CurrentItem;
            if (faculty != null)
            {
                object[] args = { faculty.Id };
                var window = ShowWindow<FacultyWindow>(args);
                window.Closed += (sender, args) => LoadData();
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var faculty = (Faculty)GridFaculties.CurrentItem;
            if (faculty != null && MessageBox.Show(this, "Вы действительно хотите удалить запись?", "Удалить", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var dataCtx = new ARMDataContext();

                dataCtx.Faculties.Remove(faculty);
                dataCtx.SaveChanges();
                LoadData();
            }
        }
    }
}
