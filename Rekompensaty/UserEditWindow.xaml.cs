using Rekompensaty.Common.DTO;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Rekompensaty
{
    /// <summary>
    /// Interaction logic for UserEditWindow.xaml
    /// </summary>
    public partial class UserEditWindow : Window, INotifyPropertyChanged
    {
        private UserDTO _user;
        public event PropertyChangedEventHandler PropertyChanged;

        public UserEditWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public UserEditWindow(UserDTO user, Window owner) : this()
        {
            User = user;
            Owner = owner;
        }

        public UserDTO User
        {
            get
            {
                return _user;
            }
            private set
            {
                _user = value;
                NotifyOnPropertyChange("User");
            }
        }

        public void NotifyOnPropertyChange(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

        private void SaveUser(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
