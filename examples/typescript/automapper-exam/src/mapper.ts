import { classes } from '@automapper/classes';
import { addProfile, createMapper } from '@automapper/core';
import { userProfile } from './mapper-profile';

export const mapper = createMapper({
  strategyInitializer: classes(),
});

addProfile(mapper, userProfile);

// 在旧的版本中
// import { UserProfile } from './mapper-profile';
//
// export const mapper = createMapper({
//   name: 'userMapper',
//   pluginInitializer: classes,
// });

// mapper.addProfile(UserProfile);
