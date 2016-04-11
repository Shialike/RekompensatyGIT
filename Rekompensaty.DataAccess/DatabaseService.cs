using Rekompensaty.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rekompensaty.DataAccess
{
    public class DatabaseService : dbModelContext
    {
        private static DatabaseService _instance;

        public static DatabaseService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DatabaseService();
                }
                return _instance;
            }
        }

        #region Database access

        public bool CheckIfDatabaseIsCorrect()
        {
            Instance.Database.Initialize(false);
            return Instance.Database.Exists();
        }

        public List<UserDTO> GetUsers()
        {
            var users = Instance.Users.ToList();
            return AutoMapperImpl.Mapper.Map<List<UserDTO>>(users);
        }

        #endregion

        #region Private Methods
        
        #endregion
    }
}
