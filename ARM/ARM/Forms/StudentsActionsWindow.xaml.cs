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
            object[] args = { null, _studentId };
            var window = ShowWindow<StudentActionWindow>(args);
            window.Closed += (sender, args) => LoadData();
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            var studentAction = (StudentAction)GridStudents.CurrentItem;
            if (studentAction != null)
            {
                object[] args = { studentAction.Id, _studentId };
                var window = ShowWindow<StudentActionWindow>(args);
                window.Closed += (sender, args) => LoadData();
            }
        }

        private void LoadData()
        {
            var dataCtx = new ARMDataContext();
            dataCtx.StudentActionTypes.Load();
            dataCtx.Students.Load();
            dataCtx.Groups.Load();

            studentActionsViewSource.Source = dataCtx.StudentActions.Where(a => a.StudentId == _studentId).ToList();
            GridStudents.UpdateLayout();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var studentAction = (StudentAction)GridStudents.CurrentItem;
            if (studentAction != null && MessageBox.Show(this, "Вы действительно хотите удалить запись?", "Удалить", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var dataCtx = new ARMDataContext();
                dataCtx.StudentActions.Remove(studentAction);
                dataCtx.SaveChanges();
                LoadData();
            }
        }

        private void SetTitle()
        {
            var dataCtx = new ARMDataContext();
            dataCtx.Groups.Load();

            var student = dataCtx.Students.SingleOrDefault(s => s.Id == _studentId);

            this.Title = $"Студент(ка) {student.LastName} {student.Name} {student.MiddleName}. Группа {student?.Group?.Title}";
        }

    }
}
