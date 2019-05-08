using Rekompensaty.Common.DTO;
using Rekompensaty.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
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
            var exists = Instance.Database.Exists();
            if (GetAnimalTypes().Count == 0)
            {
                AddAnimalType(new AnimalTypeDTO() { Name = "Jeleń Byk" });
                AddAnimalType(new AnimalTypeDTO() { Name = "Jeleń Łania" });
                AddAnimalType(new AnimalTypeDTO() { Name = "Jeleń Ciele" });
                AddAnimalType(new AnimalTypeDTO() { Name = "Daniel Byk" });
                AddAnimalType(new AnimalTypeDTO() { Name = "Daniel Łania" });
                AddAnimalType(new AnimalTypeDTO() { Name = "Daniel Ciele" });
                AddAnimalType(new AnimalTypeDTO() { Name = "Sarna Kozioł" });
                AddAnimalType(new AnimalTypeDTO() { Name = "Sarna Koza" });
                AddAnimalType(new AnimalTypeDTO() { Name = "Sarna Koźle" });
                AddAnimalType(new AnimalTypeDTO() { Name = "Dzik" });
            }
            return exists && GetAnimalTypes().Count > 0;
        }

        public void SaveAnimalTypes(List<AnimalTypeDTO> animalTypes)
        {
            foreach (var item in animalTypes)
            {
                var dbItem = Instance.AnimalTypes.SingleOrDefault(x => x.Id == item.Id.ToString());
                if (dbItem != null)
                {
                    dbItem.RevenueValue = item.RevenueValue;
                    RunCommand(() => Instance.Entry(dbItem).State = System.Data.Entity.EntityState.Modified);
                }
            }
        }

        public void EditAnimalType(AnimalTypeDTO animalType)
        {
            throw new NotImplementedException();
        }

        public UserDTO EditUser(UserDTO user)
        {
            throw new NotImplementedException();
        }

        public List<AnimalTypeDTO> GetAnimalTypes()
        {
            return AutoMapperImpl.Mapper.Map<List<AnimalTypeDTO>>(Instance.AnimalTypes.OrderBy(a => a.Name).ToList());
        }

        public void AddHuntedAnimal(HuntedAnimalDTO huntedAnimalDTO)
        {
            if (huntedAnimalDTO.Id == Guid.Empty)
            {
                huntedAnimalDTO.Id = Guid.NewGuid();
            }
            else
            {
                Instance.EditHuntedAnimal(huntedAnimalDTO);
            }
            var hunt = AutoMapperImpl.Mapper.Map<HuntedAnimal>(huntedAnimalDTO);
            hunt.AnimalTypeId = huntedAnimalDTO.AnimalType.Id.ToString();
            hunt.AnimalType = null;
            RunCommand(() => Instance.HuntedAnimals.Add(hunt));
        }

        private void EditHuntedAnimal(HuntedAnimalDTO huntedAnimalDTO)
        {
            throw new NotImplementedException();
        }

        public IList<HuntedAnimalDTO> GetHuntedAnimalsForUser(Guid userId, DateTime? startDate = default(DateTime?), DateTime? endDate = default(DateTime?))
        {
            try
            {
                var stringId = userId.ToString();
                var animalsQuery = Instance.HuntedAnimals.Where(a => a.UserId == stringId);
                var allAnimals = animalsQuery.ToList();
                if (startDate.HasValue)
                {
                    allAnimals = allAnimals.Where(a => DateTime.Parse(a.HuntDate) >= startDate.Value).ToList();
                }
                if (endDate.HasValue)
                {
                    allAnimals = allAnimals.Where(a => DateTime.Parse(a.HuntDate) <= endDate.Value).ToList();
                }
                var animals = allAnimals.OrderBy(a => a.HuntDate).ToList();
                return AutoMapperImpl.Mapper.Map<List<HuntedAnimalDTO>>(animals);
            }
            catch (Exception e)
            {
                throw e;
            }
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
            if (user.Id == Guid.Empty)
            {
                return;
            }
            RunCommand(() =>
            {
                var id = user.Id.ToString();
                var toRemove = Instance.Users.FirstOrDefault(a => a.Id == id);
                if (toRemove != null)
                {
                    foreach (var animal in toRemove.HuntedAnimals.ToList())
                    {
                        Instance.HuntedAnimals.Remove(animal);
                    }
                    Instance.Users.Remove(toRemove);
                }
            });
        }

        public void RemoveHuntedAnimal(HuntedAnimalDTO animal)
        {
            if (animal.Id == Guid.Empty)
            {
                return;
            }
            RunCommand(() =>
            {
                var id = animal.Id.ToString();
                var toRemove = Instance.HuntedAnimals.FirstOrDefault(a => a.Id == id);
                if (toRemove != null)
                {
                    Instance.HuntedAnimals.Remove(toRemove);
                }
            });
        }

        #endregion

        private void RunCommand(Action method)
        {
            RunCommand(method);
        }

        private object RunCommand(Func<object> method)
        {
            try
            {
                return method();
            }
            catch (Exception)
            {
            }
            finally
            {
                Instance.SaveChanges();
            }
            return null;
        }

        public Version GetDBVersion()
        {
            try
            {
                var ver = Instance.ProgramDatas.FirstOrDefault();
                if (ver != null)
                {
                    return new Version(ver.dbVersion);
                }
            }
            catch (Exception e)
            {

            }
            return null;
        }

        public void RunSQL(string sql)
        {
            Instance.Database.ExecuteSqlCommand(sql);
        }
    }
}
