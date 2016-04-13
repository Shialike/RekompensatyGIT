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
        #region Fields and events

        private DatabaseAccess _dbAccess = new DatabaseAccess();
        private ObservableCollection<UserDTO> _users;
        private DateTime _startDate;
        private DateTime _endDate;
        private UserDTO _selectedUser;
        private ObservableCollection<HuntedAnimalDTO> _huntedAnimals;

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Users = new ObservableCollection<UserDTO>();
            HuntedAnimals = new ObservableCollection<HuntedAnimalDTO>();
            SetInitialDate();
            FetchData();
        }

        #region Properties

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                NotifyPropertyChanged("StartDate");
                FetchHuntedAnimals();
            }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                NotifyPropertyChanged("EndDate");
                FetchHuntedAnimals();
            }
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

        public UserDTO SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                NotifyPropertyChanged("SelectedUser");
                FetchHuntedAnimals();
            }
        }

        public ObservableCollection<HuntedAnimalDTO> HuntedAnimals
        {
            get { return _huntedAnimals; }
            set
            {
                _huntedAnimals = value;
                NotifyPropertyChanged("HuntedAnimals");
            }
        }

        #endregion

        #region Methods

        public void FetchData()
        {
            var task = new Task(() =>
            {
                Users = new ObservableCollection<UserDTO>(_dbAccess.GetUsers());
                if (SelectedUser == null)
                {
                    SelectedUser = Users.FirstOrDefault();
                    FetchHuntedAnimals();
                }
            });
            task.Start();
        }

        public void FetchHuntedAnimals()
        {
            var task = new Task(() =>
            {
                if (SelectedUser != null)
                {
                    HuntedAnimals = new ObservableCollection<HuntedAnimalDTO>(_dbAccess.GetHuntedAnimalsForUser(SelectedUser.Id, StartDate, EndDate));
                }
            });
            task.Start();
        }

        private void SetInitialDate()
        {
            var seasonStart = new DateTime(DateTime.Now.Year, 4, 1);
            if (DateTime.Now >= seasonStart)
            {
                StartDate = seasonStart;
                EndDate = new DateTime(seasonStart.AddYears(1).Year, 3, 31);
            }
            else
            {
                StartDate = seasonStart.AddYears(-1);
                EndDate = new DateTime(seasonStart.Year, 3, 31);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            var userDTO = new UserDTO();
            var edtWnd = new UserEditWindow(userDTO, this);
            var result = edtWnd.ShowDialog();
            if (result.HasValue && result.Value)
            {
                _dbAccess.AddUser(userDTO);
                FetchData();
            }
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddAnimalType_Click(object sender, RoutedEventArgs e)
        {
            //_dbAccess.AddAnimalType(new AnimalTypeDTO() { Name = "Sarna" });
        }

        private void AddData_Click(object sender, RoutedEventArgs e)
        {
        }

        #endregion

        #region INotifyPropertChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;


        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion
    }
}
