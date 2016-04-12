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
        IList<UserDTO> GetUsers();
        void AddUser(UserDTO user);
        UserDTO EditUser(UserDTO user);
        void RemoveUser(UserDTO user);
        void AddAnimalType(AnimalTypeDTO animalType);
        void EditAnimalType(AnimalTypeDTO animalType);
        void RemoveAnimalType(AnimalTypeDTO animalType);
        IList<AnimalTypeDTO> GetAnimalTypes();
        IList<HuntedAnimalDTO> GetHuntedAnimalsForUser(Guid userId, DateTime? startDate = null, DateTime? endDate = null);
        
    }
}
