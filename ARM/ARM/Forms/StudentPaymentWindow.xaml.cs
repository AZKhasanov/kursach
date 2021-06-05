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
using Castle.Components.DictionaryAdapter;

namespace ARM.Forms
{
    /// <summary>
    /// Interaction logic for StudentWindow.xaml
    /// </summary>
    public partial class StudentPaymentWindow : Window
    {
        private int? _paymentId { get; set; }
        private int? _studentId { get; set; }
        private ARMDataContext _dataContext = new();
        private CollectionViewSource groupsViewSource;

        public StudentPaymentWindow(int? paymentId, int? studentId)
        {
            InitializeComponent();

            _paymentId = paymentId;
            _studentId = studentId;
            groupsViewSource = (CollectionViewSource)FindResource(nameof(groupsViewSource));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_paymentId == null)
            {
                DataContext = new StudentPayment()
                {
                    StudentId = _studentId ?? 0,
                    Date = DateTime.Now
                };
            }
            else
            {
                DataContext = _dataContext.StudentPayments.SingleOrDefault(p => p.Id == _paymentId);
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            SaveData();
        }

        private void SaveData()
        {
            var studentPayment = (StudentPayment)DataContext;

           // var student = _dataContext.Students.SingleOrDefault(s => s.Id == _studentId);



            _dataContext.Attach(studentPayment);
            _dataContext.SaveChanges();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
