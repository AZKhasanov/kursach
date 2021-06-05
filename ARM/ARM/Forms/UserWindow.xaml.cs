using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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
using Castle.Core.Internal;

namespace ARM.Forms
{
    public partial class UserWindow : Window
    {
        private int? _userId;
        private ARMDataContext _dataContext = new();
        private const string EMPTYPASSWORD = "***";
        private string _password;

        public UserWindow(int? userId = null)
        {
            InitializeComponent();

            _userId = userId;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            User user = _userId == null ? new() : _dataContext.Users.Find(_userId);
            PasswordEdit.Password = _userId == null ? "" : EMPTYPASSWORD;
            DataContext = user;
        }

        protected override void OnClosing(CancelEventArgs e)
        {

            base.OnClosing(e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveEntity();
        }

        private void SaveEntity()
        {
            var user = (User)DataContext;
            _dataContext.Users.Attach(user);

            if (_password.Equals(EMPTYPASSWORD) && _userId != null)
                _password = user.Password;
            else if (PasswordIsValid())
            {
                var md5 = MD5.Create();
                var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(_password));
                user.Password = Convert.ToBase64String(hash);
            }
            else if (PasswordIsEmpty())
            {
                MessageBox.Show(this, "Введите пароль!");
                return;
            }
            else
            {
                MessageBox.Show(this, "Допустимы только цифры и буквы. От 8 до 15 символов.", "Невалидный пароль!");
                return;
            }

            
            _dataContext.SaveChangesAsync();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded)
                _password = ((PasswordBox) sender).Password;
        }

        private bool PasswordIsEmpty() => string.IsNullOrWhiteSpace(_password);

        private bool PasswordIsValid()
        {
            var len = new Regex("^.{8,15}$");
            var regEx = new Regex("^[a-zA-Z0-9 ]*$");

            return regEx.IsMatch(_password) && len.IsMatch(_password);
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
