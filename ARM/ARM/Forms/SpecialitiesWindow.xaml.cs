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
    public partial class SpecialititesWindow : WindowBase
    {
        public CollectionViewSource specialitiesViewSource;

        public SpecialititesWindow()
        {
            InitializeComponent();
            specialitiesViewSource = (CollectionViewSource)FindResource(nameof(specialitiesViewSource));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            object[] args = { null };
            var window = ShowWindow<SpecialityWindow>(args);
            window.Closed += (sender, args) => LoadData();
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            var speciality = (Speciality)GridSpeciality.CurrentItem;
            if (speciality != null)
            {
                object[] args = { speciality.Id };
                var window = ShowWindow<SpecialityWindow>(args);
                window.Closed += (sender, args) => LoadData();
            }
        }

        private void LoadData()
        {
            var dataCtx = new ARMDataContext();
            dataCtx.Departments.Load();
            dataCtx.Faculties.Load();

            specialitiesViewSource.Source = dataCtx.Specialities.ToList();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var speciality = (Speciality)GridSpeciality.CurrentItem;
            if (speciality != null && MessageBox.Show(this, "Вы действительно хотите удалить запись?", "Удалить", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var dataCtx = new ARMDataContext();

                dataCtx.Specialities.Remove(speciality);
                dataCtx.SaveChanges();
                LoadData();
            }
        }
    }
}
