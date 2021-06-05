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
    public partial class StudentsPaymentsWindow : WindowBase
    {
        public CollectionViewSource studentPaymentsViewSource;
        private int? _studentId;

        public StudentsPaymentsWindow(int? studentId)
        {
            InitializeComponent();
            studentPaymentsViewSource = (CollectionViewSource)FindResource(nameof(studentPaymentsViewSource));
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
            var window = ShowWindow<StudentPaymentWindow>(args);
            window.Closed += (sender, args) => LoadData();
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
/*            var studentAction = (StudentAction)StudentsGrid.CurrentItem;
            if (studentAction != null)
            {
                object[] args = { studentAction.Id, _studentId };
                var window = ShowWindow<StudentActionWindow>(args);
                window.Closed += (sender, args) => LoadData();
            }*/
        }

        private void LoadData()
        {
            var dataCtx = new ARMDataContext();
            dataCtx.Students.Load();
            dataCtx.Groups.Load();

            studentPaymentsViewSource.Source = dataCtx.StudentPayments.Where(a => a.StudentId == _studentId).ToList();
            StudentsGrid.UpdateLayout();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var payment = (StudentPayment)StudentsGrid.CurrentItem;
            if (payment != null && MessageBox.Show(this, "Вы действительно хотите удалить запись?", "Удалить", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var dataCtx = new ARMDataContext();
                dataCtx.StudentPayments.Remove(payment);
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
