﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rekompensaty.Common.DTO;

namespace Rekompensaty.DataAccess
{
    public class DatabaseAccess : IDatabaseMethods
    {
        public void AddAnimalType(AnimalTypeDTO animalType)
        {
            DatabaseService.Instance.AddAnimalType(animalType);
        }

        public void AddUser(UserDTO user)
        {
            DatabaseService.Instance.AddUser(user);
        }

        public void EditAnimalType(AnimalTypeDTO animalType)
        {
            DatabaseService.Instance.EditAnimalType(animalType);
        }

        public UserDTO EditUser(UserDTO user)
        {
            return DatabaseService.Instance.EditUser(user);
        }

        public IList<AnimalTypeDTO> GetAnimalTypes()
        {
            return DatabaseService.Instance.GetAnimalTypes();
        }

        public IList<HuntedAnimalDTO> GetHuntedAnimalsForUser(Guid userId, DateTime? startDate = default(DateTime?), DateTime? endDate = default(DateTime?))
        {
            return DatabaseService.Instance.GetHuntedAnimalsForUser(userId, startDate, endDate);
        }

        public IList<UserDTO> GetUsers()
        {
            return DatabaseService.Instance.GetUsers();
        }

        public void RemoveAnimalType(AnimalTypeDTO animalType)
        {
            DatabaseService.Instance.RemoveAnimalType(animalType);
        }

        public void RemoveUser(UserDTO user)
        {
            DatabaseService.Instance.RemoveUser(user);
        }
    }
}