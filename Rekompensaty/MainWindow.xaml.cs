using Microsoft.Reporting.WebForms;
using Rekompensaty.Common.DTO;
using Rekompensaty.DataAccess;
using SUT.PrintEngine.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
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
        private HuntedAnimalDTO _selectedRow;

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

        public double Suma
        {
            get
            {
                return HuntedAnimals.Sum(x => x.Revenue);
            }
        }

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
                NotifyPropertyChanged("CanAddData");
                NotifyPropertyChanged("SummaryString");
                FetchHuntedAnimals();
            }
        }

        public HuntedAnimalDTO SelectedRow
        {
            get { return _selectedRow; }
            set
            {
                _selectedRow = value;
                NotifyPropertyChanged("SelectedRow");
                NotifyPropertyChanged("CanRemoveData");
            }
        }

        public ObservableCollection<HuntedAnimalDTO> HuntedAnimals
        {
            get { return _huntedAnimals; }
            set
            {
                _huntedAnimals = value;
                NotifyPropertyChanged("HuntedAnimals");
                NotifyPropertyChanged("Suma");
            }
        }

        public bool CanAddData
        {
            get { return SelectedUser != null; }
        }

        public bool CanRemoveData
        {
            get { return SelectedRow != null; }
        }

        public string SummaryString
        {
            get
            {
                return SelectedUser.FullName + " suma rekompensat: ";
            }
        }

        #endregion

        #region Methods

        public void FetchData()
        {
            Users = new ObservableCollection<UserDTO>(_dbAccess.GetUsers());
            if (SelectedUser == null)
            {
                SelectedUser = Users.FirstOrDefault();
                FetchHuntedAnimals();
            }
        }

        public void FetchHuntedAnimals()
        {
            if (SelectedUser != null)
            {
                try
                {
                    HuntedAnimals = new ObservableCollection<HuntedAnimalDTO>(_dbAccess.GetHuntedAnimalsForUser(SelectedUser.Id, StartDate, EndDate));
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message + "\n" + e.InnerException ?? "");
                }
            }
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

        private void RemoveUser_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedUser != null && MessageBoxResult.Yes == MessageBox.Show("Na pewno chcesz usunąć myśliwego oraz wszystkie powiązane z nim dane?", "Pytanie", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                _dbAccess.RemoveUser(SelectedUser);
                Users.Remove(SelectedUser);
                SelectedUser = Users.FirstOrDefault();
            }
        }

        private void RemoveData_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedRow != null && MessageBoxResult.Yes == MessageBox.Show("Na pewno chcesz usunąć dane polowania?", "Pytanie", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                _dbAccess.RemoveHuntedAnimal(SelectedRow);
                HuntedAnimals.Remove(SelectedRow);
                SelectedRow = null;
            }
        }

        private void AddData_Click(object sender, RoutedEventArgs e)
        {
            var huntedAnimalDTO = new HuntedAnimalDTO();
            var animalTypes = _dbAccess.GetAnimalTypes();
            var edtWnd = new NewHuntWindow(animalTypes.ToList(), SelectedUser, huntedAnimalDTO, this);
            var result = edtWnd.ShowDialog();
            if (result.HasValue && result.Value)
            {
                _dbAccess.AddHuntedAnimal(huntedAnimalDTO);
                var selectedIdx = Users.IndexOf(SelectedUser);
                FetchData();
                SelectedUser = Users[selectedIdx];
            }
        }

        private void PrintData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var visualSize = new Size(dataGrid.ActualWidth, dataGrid.ActualHeight);
                var printControl = PrintControlFactory.Create(visualSize, dataGrid);
                printControl.ShowPrintPreview();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Wystąpił błąd podczas próby drukowania!\n {0}", ex.Message), "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void ChangeSettings_Click(object sender, RoutedEventArgs e)
        {
            var animalTypes = _dbAccess.GetAnimalTypes();
            var edtWnd = new SettingsWindow(animalTypes.ToList(), this);
            var result = edtWnd.ShowDialog();
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
