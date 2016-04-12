using Rekompensaty.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Rekompensaty
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            AutoMapperImpl.Configure();
            try
            {
                var dbAccess = new DatabaseAccess();
                if(!dbAccess.CheckIfDatabaseIsCorrect())
                {
                    NoDBError();
                }
            }
            catch
            {
                NoDBError();
            }
        }

        private static void NoDBError()
        {
            MessageBox.Show("Baza danych jest niepoprawna!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            Current.Shutdown();
        }
    }
}
