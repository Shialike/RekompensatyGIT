using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Rekompensaty.Common.DTO;

namespace Rekompensaty.DataAccess
{
    public static class AutoMapperImpl
    {
        private static IMapper _mapper;

        public static void Configure()
        {
            _mapper = CreateMapper();
        }

        private static IMapper CreateMapper()
        {
            var config = new MapperConfiguration((mapper) =>
            {
                mapper.CreateMap<User, UserDTO>();
                mapper.CreateMap<UserDTO, User>();                
            });
            return config.CreateMapper();
        }

        public static IMapper Mapper
        {
            get
            {
                if (_mapper == null)
                {
                    _mapper = CreateMapper();
                }
                return _mapper;
            }
        }
    }
}
