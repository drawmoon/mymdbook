# TypeScript 使用 AutoMapper 实现对象与对象自动映射

安装

```bash
npm install @automapper/core @automapper/classes reflect-metadata
npm install -D @automapper/types
```

使用 `AutoMap` 装饰器装饰属性

```typescript
import { AutoMap } from "@automapper/classes";

export class User {
  @AutoMap()
  id: number;

  @AutoMap()
  username: string;

  @AutoMap()
  email: string;

  password: string;

  @AutoMap()
  a1: string;
}

export class UserDTO {
  @AutoMap()
  id: number;

  @AutoMap()
  username: string;

  @AutoMap()
  email: string;

  password: string;

  @AutoMap()
  a2: string;
}
```

创建 `Profile`，新建 `mapper-profile.ts` 文件

```typescript
import { CamelCaseNamingConvention, mapFrom } from "@automapper/core";
import type { MappingProfile } from "@automapper/types";
import { UserDTO } from "../dtos/user.dto";
import { User } from "../entities/user";

export const UserProfile: MappingProfile = (mapper) => {
  mapper.createMap(User, UserDTO, {
    namingConventions: {
      source: new CamelCaseNamingConvention(),
      destination: new CamelCaseNamingConvention(),
    }
  })
  .forMember((user) => user.a2, mapFrom((source) => source.a1)); // 自定义成员的配置，a1 -> a2
}
```

创建 `Mapper`，新建 `mapper.ts` 文件

```typescript
import { classes } from "@automapper/classes";
import { createMapper } from "@automapper/core";
import { UserProfile } from "./mapper-profile";

export const Mapper = createMapper({
  name: 'userMapper',
  pluginInitializer: classes,
});

Mapper.addProfile(UserProfile);
```

使用 `Mapper`

```ts
const users: User[] = [
  {
    id: 1,
    username: 'drsh',
    email: '1340260725@qq.com',
    password: '123',
    a1: 'a1',
  }
];
return Mapper.mapArray(users, UserDTO, User);

// 输出结果：[{"id":1,"username":"drsh","email":"1340260725@qq.com","a2":"a1"}]
```
