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
using FastReport;

namespace ARM.Forms
{
    /// <summary>
    /// Interaction logic for StudentActionsReportWindow.xaml
    /// </summary>
    public partial class StudentActionsReportWindow : Window
    {
        private ARMDataContext _dataContext = new();
        private int _studentId;

        public StudentActionsReportWindow(int studentId)
        {
            InitializeComponent();

            _studentId = studentId;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            _dataContext.Students.Load();
            _dataContext.Groups.Load();
            _dataContext.Specialities.Load();
            _dataContext.Departments.Load();
            _dataContext.Faculties.Load();
            _dataContext.StudentActions.Load();

            var student = _dataContext.Students.SingleOrDefault(s => s.Id == _studentId);

            this.Title = $"Отчёт по студенту(ке) {student.LastName} {student.Name} {student.MiddleName}";

            LabelStudentFIO.Content += $"{student.LastName} {student.Name} {student.MiddleName}";
            LabelStudentSpeciality.Content += $"{student?.Group?.Speciality?.Title}";
            LabelStudentDepartment.Content += $"{student?.Group?.Speciality?.Department?.Title}";
            LabelStudentGroup.Content += $"{student?.Group?.Title}";
            LabelStudentFaculty.Content += $"{student?.Group?.Speciality?.Department?.Faculty?.Title}";
            LabelStudentSpecialityCost.Content += $"{student?.Group?.Speciality?.Cost}";

            var studentAction = _dataContext.StudentActions
                .Where(a => a.StudentId == _studentId)
                .OrderByDescending(a => a.DateBegin)
                .FirstOrDefault();

            LabelStudentStatus.Content += $"{studentAction.Type} {studentAction.DateBegin}";

            var payments = _dataContext.StudentPayments
                .Where(p => p.StudentId == _studentId)
                .Select(p => p.Amount)
                .Sum();

            LabelStudentPayments.Content += payments.ToString();
        }

        public void Print()
        {
            System.Windows.Controls.PrintDialog Printdlg = new System.Windows.Controls.PrintDialog();
            if ((bool)Printdlg.ShowDialog().GetValueOrDefault())
            {
                Size pageSize = new Size(Printdlg.PrintableAreaWidth, Printdlg.PrintableAreaHeight);
                // sizing of the element.
                GridReport.Measure(pageSize);
                GridReport.Arrange(new Rect(5, 5, pageSize.Width, pageSize.Height));
                Printdlg.PrintVisual(GridReport, Title);
            }
        }

        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {
            Print();
        }
    }
}
