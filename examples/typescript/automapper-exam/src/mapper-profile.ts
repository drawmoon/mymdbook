import { UserDTO } from './user.dto';
import { User } from './user';

import { mapFrom, createMap, forMember, MappingProfile } from '@automapper/core';

export const userProfile: MappingProfile = (mapper) => {
  createMap(
    mapper,
    User,
    UserDTO,
    forMember(
      (destination) => destination.fullName,
      mapFrom((source) => source.firstName + source.lastName)
    ),
  );
};

// 在旧的版本中
// import { CamelCaseNamingConvention } from '@automapper/core';
//
// export const UserProfile: MappingProfile = (mapper) => {
//   mapper
//     .createMap(User, UserDTO, {
//       namingConventions: {
//         source: new CamelCaseNamingConvention(),
//         destination: new CamelCaseNamingConvention(),
//       }
//     })
//     .forMember(
//       (destination) => destination.fullName, 
//       mapFrom((source) => source.firstName + source.lastName));
// }
