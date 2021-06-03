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

namespace ARM.Forms
{
    /// <summary>
    /// Interaction logic for StudentStateWindow.xaml
    /// </summary>
    public partial class StudentActionWindow : Window
    {
        private int? _actionId;
        private CollectionViewSource typesViewSource;
        private ARMDataContext _dataContext = new();


        public StudentActionWindow(int? actionId)
        {
            InitializeComponent();
            _actionId = actionId;
            typesViewSource = (CollectionViewSource) FindResource(nameof(typesViewSource));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            typesViewSource.Source = _dataContext.StudentActionTypes.ToList();
            DataContext = _actionId == null ? new(){DateBegin = DateTime.Now} :_dataContext.StudentActions.SingleOrDefault(a => a.Id == _actionId);
        }
    }
}
