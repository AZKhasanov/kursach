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
    /// Interaction logic for Users.xaml
    /// </summary>
    public partial class UsersWindow : WindowBase
    {
        private CollectionViewSource usersViewSource;

        public UsersWindow()
        {
            InitializeComponent();

            usersViewSource = (CollectionViewSource) FindResource(nameof(usersViewSource));
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var dataCtx = new ARMDataContext();
            usersViewSource.Source = dataCtx.Users.ToList();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {

            base.OnClosing(e);
        }

        private void UsersTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            object[] args = {null};
            ShowWindow<UserWindow>(args);
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            var user = (User)GridUsers.CurrentItem;
            if (user != null)
            {
                object[] args = {user.Id};
                ShowWindow<UserWindow>(args);
            }
        }
    }
}
