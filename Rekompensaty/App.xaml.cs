using Rekompensaty.Common;
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
                if (!dbAccess.CheckIfDatabaseIsCorrect())
                {
                    NoDBError("");
                }
                var version = dbAccess.GetDBVersion();
                if (version == null || version != new Version(Constants.DbVersion))
                {
                    NoDBError($"Nieprawidłowa wersja bazy! Jest: {version} - oczekiwano: {Constants.DbVersion}");
                }
            }
            catch (Exception ex)
            {
                NoDBError(ex.Message);
            }
        }

        private static void NoDBError(string message)
        {
            MessageBox.Show(string.Format("Wystąpił błąd podczas uruchamiania programu!\n {0}", message), "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            Current.Shutdown();
        }
    }
}
