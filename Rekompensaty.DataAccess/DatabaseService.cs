using Rekompensaty.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rekompensaty.DataAccess
{
    internal class DatabaseService : dbModelContext, IDatabaseMethods
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

        public void AddAnimalType(AnimalTypeDTO animalType)
        {
            throw new NotImplementedException();
        }

        public void AddUser(UserDTO user)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfDatabaseIsCorrect()
        {
            Instance.Database.Initialize(false);
            return Instance.Database.Exists();
        }

        public void EditAnimalType(AnimalTypeDTO animalType)
        {
            throw new NotImplementedException();
        }

        public UserDTO EditUser(UserDTO user)
        {
            throw new NotImplementedException();
        }

        public IList<AnimalTypeDTO> GetAnimalTypes()
        {
            throw new NotImplementedException();
        }

        public IList<HuntedAnimalDTO> GetHuntedAnimalsForUser(Guid userId, DateTime? startDate = default(DateTime?), DateTime? endDate = default(DateTime?))
        {
            throw new NotImplementedException();
        }

        public List<UserDTO> GetUsers()
        {
            var users = Instance.Users.ToList();
            return AutoMapperImpl.Mapper.Map<List<UserDTO>>(users);
        }

        public void RemoveAnimalType(AnimalTypeDTO animalType)
        {
            throw new NotImplementedException();
        }

        public void RemoveUser(UserDTO user)
        {
            throw new NotImplementedException();
        }

        IList<UserDTO> IDatabaseMethods.GetUsers()
        {
            throw new NotImplementedException();
        }

        #endregion
        
    }
}
