using System;
using System.Collections.Generic;
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
    /// Interaction logic for StudentWindow.xaml
    /// </summary>
    public partial class SpecialityWindow : Window
    {
        private int? _studentId { get; set; }
        private ARMDataContext _dataContext = new();
        private CollectionViewSource departmentsViewSource;

        public SpecialityWindow(int? studentId)
        {
            InitializeComponent();

            _studentId = studentId;
            departmentsViewSource = (CollectionViewSource)FindResource(nameof(departmentsViewSource));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = _studentId == null ? new Speciality() : _dataContext.Specialities.Find(_studentId);
            departmentsViewSource.Source = _dataContext.Departments.ToList();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            SaveData();
        }

        private void SaveData()
        {
            var speciality = (Speciality)DataContext;
            _dataContext.Specialities.Attach(speciality);
            _dataContext.SaveChanges();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
