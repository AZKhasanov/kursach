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
    /// Interaction logic for GroupsWindow.xaml
    /// </summary>
    public partial class GroupsWindow : WindowBase
    {
        private CollectionViewSource groupdViewSource;

        public GroupsWindow()
        {
            InitializeComponent();

            groupdViewSource = (CollectionViewSource) FindResource(nameof(groupdViewSource));
        }

        private void Window_Loaded(object sender, RoutedEventArgs args)
        {
            LoadData();
        }

        private void LoadData()
        {
            var dataCtx = new ARMDataContext();

            dataCtx.Employees.Load();
            dataCtx.Specialities.Load();
            dataCtx.Departments.Load();
            dataCtx.Faculties.Load();

            groupdViewSource.Source = dataCtx.Groups.ToList();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            object[] args = { null };
            var window = ShowWindow<GroupWindow>(args);
            window.Closed += (sender, args) => LoadData();
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            var group = (Group)GridGroups.CurrentItem;
            if (group != null)
            {
                object[] args = { group.Id };
                var window = ShowWindow<GroupWindow>(args);
                window.Closed += (sender, args) => LoadData();
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var group = (Group)GridGroups.CurrentItem;
            if (group != null && MessageBox.Show(this, "Вы действительно хотите удалить запись?", "Удалить", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var dataCtx = new ARMDataContext();
                dataCtx.Groups.Remove(group);
                dataCtx.SaveChanges();
                LoadData();
            }
        }
    }
}
