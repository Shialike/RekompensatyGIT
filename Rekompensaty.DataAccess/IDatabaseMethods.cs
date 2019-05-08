using Rekompensaty.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rekompensaty.DataAccess
{
    public interface IDatabaseMethods
    {
        bool CheckIfDatabaseIsCorrect();
        List<UserDTO> GetUsers();
        void AddUser(UserDTO user);
        UserDTO EditUser(UserDTO user);
        void RemoveUser(UserDTO user);
        void AddAnimalType(AnimalTypeDTO animalType);
        void EditAnimalType(AnimalTypeDTO animalType);
        void RemoveAnimalType(AnimalTypeDTO animalType);
        List<AnimalTypeDTO> GetAnimalTypes();
        IList<HuntedAnimalDTO> GetHuntedAnimalsForUser(Guid userId, DateTime? startDate = null, DateTime? endDate = null);
        void RemoveHuntedAnimal(HuntedAnimalDTO animal);
        Version GetDBVersion();
        void RunSQL(string sql);
        void SaveAnimalTypes(List<AnimalTypeDTO> animalTypes);

    }
}
