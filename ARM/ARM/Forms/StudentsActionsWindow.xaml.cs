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
    public partial class StudentsActionsWindow : WindowBase
    {
        private ARMDataContext _dataContext = new();
        public CollectionViewSource studentActionsViewSource;
        private int? _studentId;

        public StudentsActionsWindow(int? studentId)
        {
            InitializeComponent();
            studentActionsViewSource = (CollectionViewSource)FindResource(nameof(studentActionsViewSource));
            _studentId = studentId;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
            SetTitle();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            object[] args = { null };
            var window = ShowWindow<StudentActionWindow>(args);
            window.Closed += (sender, args) => LoadData();
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            var studentAction = (StudentAction)StudentsGrid.CurrentItem;
            if (studentAction != null)
            {
                object[] args = { studentAction.Id };
                var window = ShowWindow<StudentActionWindow>(args);
                window.Closed += (sender, args) => LoadData();
            }
        }

        private void LoadData()
        {
            _dataContext.StudentActionTypes.Load();
            _dataContext.Students.Load();
            _dataContext.Groups.Load();

            studentActionsViewSource.Source = _dataContext.StudentActions.Where(a => a.StudentId == _studentId).ToList();
            StudentsGrid.UpdateLayout();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var studentAction = (StudentAction)StudentsGrid.CurrentItem;
            if (studentAction != null && MessageBox.Show(this, "Вы действительно хотите удалить запись?", "Удалить", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _dataContext.StudentActions.Remove(studentAction);
                _dataContext.SaveChanges();
                LoadData();
            }
        }

        private void SetTitle()
        {
            var student = _dataContext.Students.SingleOrDefault(s => s.Id == _studentId);

            this.Title = $"Студент(ка) {student.LastName} {student.Name} {student.MiddleName}. Группа {student.Group.Title}";
        }

    }
}
