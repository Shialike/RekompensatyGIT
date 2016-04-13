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
            if (animalType.Id == Guid.Empty)
            {
                animalType.Id = Guid.NewGuid();
            }
            else
            {
                Instance.EditAnimalType(animalType);
            }
            RunCommand(() => Instance.AnimalTypes.Add(AutoMapperImpl.Mapper.Map<AnimalType>(animalType)));
        }

        public void AddUser(UserDTO user)
        {
            if (user.Id == Guid.Empty)
            {
                user.Id = Guid.NewGuid();
            }
            else
            {
                Instance.EditUser(user);
            }
            RunCommand(() => Instance.Users.Add(AutoMapperImpl.Mapper.Map<User>(user)));
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
            try
            {
                var animalsQuery = Instance.HuntedAnimals.Where(a => a.UserId == userId);
                if (startDate.HasValue)
                {
                    animalsQuery = animalsQuery.Where(a => a.HuntDate >= startDate.Value);
                }
                if (endDate.HasValue)
                {
                    animalsQuery = animalsQuery.Where(a => a.HuntDate <= endDate.Value);
                }
                var animals = animalsQuery.OrderBy(a => a.HuntDate).ToList();
                return AutoMapperImpl.Mapper.Map<List<HuntedAnimalDTO>>(animals);
            }
            catch (Exception)
            {
            }
            return new List<HuntedAnimalDTO>();
        }

        public List<UserDTO> GetUsers()
        {
            var users = Instance.Users.OrderBy(a => a.Surname).ThenBy(a => a.Name).ToList();
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
        
        private void RunCommand(Action method)
        {
            try
            {
                method();
            }
            catch(Exception)
            {
            }
            finally
            {
                Instance.SaveChanges();
            }
        }
    }
}
