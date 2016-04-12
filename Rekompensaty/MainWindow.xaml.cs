using Rekompensaty.Common.DTO;
using Rekompensaty.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Rekompensaty
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private DatabaseAccess _dbAccess = new DatabaseAccess();
        private ObservableCollection<UserDTO> _users;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Users = new ObservableCollection<UserDTO>();
        }

        public void FetchData()
        {
            var task = new Task(() =>
            {
                Users = new ObservableCollection<UserDTO>(_dbAccess.GetUsers());
            });
            task.Start();
        }

        public ObservableCollection<UserDTO> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                NotifyPropertyChanged("Users");
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        
        private void AddUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveUser_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
