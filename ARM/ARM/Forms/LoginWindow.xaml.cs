using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private LoginInfo _loginInfo = new();
        private ARMDataContext _dataContext = new();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs args)
        {
            DataContext = _loginInfo;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded)
                _loginInfo.Password = ((PasswordBox)sender).Password;
        }

        private void Login()
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(_loginInfo.Password));
            var pwd = Convert.ToBase64String(hash);

            var userExists = _dataContext.Users.Any(u => u.Name == _loginInfo.Login && u.Password == pwd);
            if (userExists)
            {
                this.DialogResult = userExists;
                Close();
            }
            else
                MessageBox.Show("Пользователь не найден или ввели неправильные лоигн или пароль!");
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }
    }
}
