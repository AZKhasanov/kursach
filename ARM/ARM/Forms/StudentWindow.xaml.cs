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
    public partial class StudentWindow : Window
    {
        private int? _studentId { get; set; }
        private ARMDataContext _dataContext = new();
        private CollectionViewSource groupsViewSource;

        public StudentWindow(int? studentId)
        {
            InitializeComponent();

            _studentId = studentId;
            groupsViewSource = (CollectionViewSource)FindResource(nameof(groupsViewSource));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = _studentId == null ? new Student() : _dataContext.Students.Find(_studentId);
            groupsViewSource.Source = _dataContext.Groups.ToList();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            SaveData();
        }

        private void SaveData()
        {
            var student = DataContext;
            _dataContext.Attach(student);
            _dataContext.SaveChanges();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var student = (Student)DataContext;
            var hasGroupHead = _dataContext.Students.Any(s => s.IsGroupHead && s.GroupId == student.GroupId);
            CheckBox.IsEnabled = !hasGroupHead;
        }
    }
}
