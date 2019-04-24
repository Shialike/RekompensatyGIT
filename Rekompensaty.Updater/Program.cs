using Rekompensaty.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Rekompensaty.Updater
{
    class Program
    {
        static void Main(string[] args)
        {
            Update();
        }

        private static void Update()
        {
            var _dbAccess = new DatabaseAccess();
            var dbversion = _dbAccess.GetDBVersion();
            var currentVersion = new Version(0, 0, 0, 0);
            try { 
                using (XmlReader reader = XmlReader.Create(@"update.xml"))
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            //return only when you have START tag  
                            switch (reader.Name.ToString())
                            {
                                case "Updates":
                                    Console.WriteLine("Aktualizacja bazy...");
                                    Console.WriteLine($"Aktualna wersja: {dbversion ?? currentVersion}");
                                    break;
                                case "Version":
                                    currentVersion = new Version(reader.GetAttribute("v"));
                                    if (dbversion == null || currentVersion > dbversion)
                                    {
                                        var sql =  reader.ReadElementContentAsString();
                                        Console.WriteLine($"Aktualizacja do wersji: {currentVersion.ToString()}");
                                        _dbAccess.RunSQL(sql);                                    
                                    }
                                    break;
                            }
                        }
                    }
                }
                _dbAccess.RunSQL($"UPDATE 'ProgramData' SET dbVersion = '{currentVersion.ToString()}'");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

            }
            Console.WriteLine("Baza aktualna\nWciśnij dowolny przycisk...");
            Console.ReadKey();
        }
    }
}
