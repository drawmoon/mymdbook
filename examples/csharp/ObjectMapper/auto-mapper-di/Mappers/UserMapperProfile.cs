using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ObjectMapper.Entities;
using ObjectMapper.Dto;

namespace ObjectMapper.Mappers
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            // Entity to DTO
            CreateMap<User, UserDto>(MemberList.Destination)
                .ForMember(destination => destination.FullName, options => {
                    options.MapFrom(source => source.FirstName + source.LastName);
                });

            // DTO to Entity
            CreateMap<UserDto, User>(MemberList.Source);
        }
    }
}
