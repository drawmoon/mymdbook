# 使用 AutoMapper 实现对象映射

- [快速入门](#快速入门)
- [自定义映射规则](#自定义映射规则)

## 快速入门

安装

```shell
npm install @automapper/core @automapper/classes reflect-metadata
```

创建实体对象与 DTO 模型对象，使用 `AutoMap` 装饰器装饰需要映射的属性：

```typescript
import { AutoMap } from '@automapper/classes';

export class User {
  @AutoMap()
  id: number;

  @AutoMap()
  firstName: string;

  @AutoMap()
  lastName: string;

  @AutoMap()
  email: string;

  password: string;
}

export class UserDto {
  @AutoMap()
  id: number;

  @AutoMap()
  fullName: string;

  @AutoMap()
  email: string;
}
```

创建映射关系：

```typescript
import { UserDTO } from './user.dto';
import { User } from './user';
import { mapFrom, createMap, forMember, MappingProfile } from '@automapper/core';

export const userProfile: MappingProfile = (mapper) => {
  createMap(mapper, User, UserDTO);
};
```

创建 `Mapper`，并将 `userProfile` 对象添加到 `Mapper`：

```typescript
import { classes } from '@automapper/classes';
import { addProfile, createMapper } from '@automapper/core';
import { userProfile } from './mapper-profile';

export const mapper = createMapper({
  strategyInitializer: classes(),
});

addProfile(mapper, userProfile);
```

现在可以使用 `Mapper` 接口提供的方法实现对象与对象的转换：

```typescript
const users: User[] = [
  {
    id: 1,
    firstName: 'hu',
    lastName: 'hongqi',
    email: 'hongqi.hu@email.com',
    password: '123456',
  }
];
return mapper.mapArray(users, User, UserDto);
```

## 自定义映射规则

通过 `forMember` 方法自定义配置属性的映射规则：

```typescript
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
```