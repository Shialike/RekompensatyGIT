using Rekompensaty.Common.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
    /// Interaction logic for NewHuntWindow.xaml
    /// </summary>
    public partial class NewHuntWindow : Window, INotifyPropertyChanged
    {
        private HuntedAnimalDTO _huntedAnimal;
        private UserDTO _hunter;
        private ObservableCollection<AnimalTypeDTO> _animalTypes;
        private ObservableCollection<int> _huntingAreas;

        public event PropertyChangedEventHandler PropertyChanged;

        public NewHuntWindow(List<AnimalTypeDTO> animalTypes, UserDTO hunter, HuntedAnimalDTO huntedAnimal, Window owner)
        {
            AnimalTypes = new ObservableCollection<AnimalTypeDTO>(animalTypes);
            Hunter = hunter;
            HuntedAnimal = huntedAnimal;
            HuntedAnimal.UserId = hunter.Id;
            SelectedAnimalType = AnimalTypes.First();
            HuntDate = DateTime.Now;
            HuntingAreas = new ObservableCollection<int>() { 37, 60 };
            HuntingArea = 37;
            DataContext = this;
            Owner = owner;
            InitializeComponent();
        }

        public void NotifyOnPropertyChange(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        public HuntedAnimalDTO HuntedAnimal
        {
            get { return _huntedAnimal; }
            set
            {
                _huntedAnimal = value;
                NotifyOnPropertyChange("HuntedAnimal");
            }
        }

        public UserDTO Hunter
        {
            get { return _hunter; }
            set
            {
                _hunter = value;
                NotifyOnPropertyChange("Hunter");
            }
        }

        public string HunterName
        {
            get
            {
                return "Myśliwy: " + Hunter.FullName;
            }
        }

        public ObservableCollection<AnimalTypeDTO> AnimalTypes
        {
            get { return _animalTypes; }
            set
            {
                _animalTypes = value;
                NotifyOnPropertyChange("AnimalTypes");
            }
        }

        [Required]
        public AnimalTypeDTO SelectedAnimalType
        {
            get { return HuntedAnimal.AnimalType; }
            set
            {
                HuntedAnimal.AnimalType = value;
                HuntedAnimal.RevenueValue = value.RevenueValue;
                NotifyOnPropertyChange("SelectedAnimalType");
                NotifyOnPropertyChange("CanSave");
            }
        }

        [Required]
        public double Weight
        {
            get { return HuntedAnimal.Weight; }
            set
            {
                HuntedAnimal.Weight = value;
                NotifyOnPropertyChange("Weight");
                NotifyOnPropertyChange("CanSave");
            }
        }

        [Required]
        public decimal Price
        {
            get { return HuntedAnimal.PricePerKilo; }
            set
            {
                HuntedAnimal.PricePerKilo = value;
                NotifyOnPropertyChange("Price");
                NotifyOnPropertyChange("CanSave");
            }
        }

        [Required]
        public int HuntingArea
        {
            get { return HuntedAnimal.HuntingArea; }
            set
            {
                HuntedAnimal.HuntingArea = value;
                NotifyOnPropertyChange("HuntingArea");
                NotifyOnPropertyChange("CanSave");
            }
        }

        public ObservableCollection<int> HuntingAreas
        {
            get { return _huntingAreas; }
            set
            {
                _huntingAreas = value;
                NotifyOnPropertyChange("HuntingAreas");
                NotifyOnPropertyChange("HuntingArea");
            }
        }

        public DateTime HuntDate
        {
            get { return HuntedAnimal.HuntDate; }
            set
            {
                HuntedAnimal.HuntDate = value;
                NotifyOnPropertyChange("HuntDate");
                NotifyOnPropertyChange("CanSave");
            }
        }

        public bool CanSave
        {
            get { return HuntDate != null && HuntDate > DateTime.MinValue && Price > 0 && Weight > 0 && SelectedAnimalType != null && (HuntingArea == 37 || HuntingArea == 60); }
        }

    }
}
