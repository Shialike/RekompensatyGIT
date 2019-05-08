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
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window, INotifyPropertyChanged
    {
        private List<AnimalTypeDTO> _animalTypes;
        private AnimalTypeDTO _selectedAnimalType;

        public SettingsWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public SettingsWindow(List<AnimalTypeDTO> animalTypes, Window owner) : this()
        {
            _animalTypes = animalTypes;
            Owner = owner;
            SelectedAnimalType = _animalTypes.FirstOrDefault();
        }

        public List<AnimalTypeDTO> AnimalTypes => _animalTypes;

        public AnimalTypeDTO SelectedAnimalType
        {
            get { return _selectedAnimalType; }
            set
            {
                _selectedAnimalType = value;
                NotifyPropertyChanged("SelectedAnimalType");
                NotifyPropertyChanged("SelectedRevenue");
            }
        }

        public double SelectedRevenue
        {
            get { return SelectedAnimalType.RevenueValue; }
            set
            {
                SelectedAnimalType.RevenueValue = value;
                NotifyPropertyChanged("SelectedRevenue");
            }
        }

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

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
