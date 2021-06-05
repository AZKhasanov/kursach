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
    /// Interaction logic for StudentStateWindow.xaml
    /// </summary>
    public partial class StudentActionWindow : Window
    {
        private int? _actionId;
        private int? _studentId;
        private CollectionViewSource typesViewSource;
        private ARMDataContext _dataContext = new();


        public StudentActionWindow(int? actionId, int? studentId)
        {
            InitializeComponent();
            
            _actionId = actionId;
            _studentId = studentId;

            typesViewSource = (CollectionViewSource) FindResource(nameof(typesViewSource));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            typesViewSource.Source = _dataContext.StudentActionTypes.ToList();
            if (_actionId == null)
            {
                DataContext = new StudentAction()
                {
                    StudentId = _studentId ?? 0,
                    DateBegin = DateTime.Now
                };
            }
            else
            {
               DataContext = _dataContext.StudentActions.SingleOrDefault(a => a.Id == _actionId);
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            var action = (StudentAction) DataContext;
            _dataContext.Attach(action);
            _dataContext.SaveChanges();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
