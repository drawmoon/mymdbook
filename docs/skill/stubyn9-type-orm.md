# TypeORM 入门

- [使用 TypeORM](#使用-typeorm)

## 使用 TypeORM

安装

> 本文使用的数据库是 `Postgres`，如果使用的是 `MySQL`，则是 `npm install --save typeorm mysql`。

```bash
npm install --save typeorm pg
```

新建 `db.providers.ts` 文件，与数据库建立连接。

```typescript
import { createConnection } from 'typeorm';

export const databaseProviders = [
  {
    provide: 'DATABASE_CONNECTION',
    useFactory: async () =>
      await createConnection({
        type: 'postgres',
        host: 'localhost',
        port: 5432,
        username: 'postgres',
        password: 'postgres',
        database: 'postgres',
        entities: [__dirname + '/../**/*.entity{.ts,.js}'],
        synchronize: true,
      }),
  },
];
```

新建 `db.module.ts` 文件，使数据库提供程序可以被其他模块导入。

```typescript
import { Module } from '@nestjs/common';

@Module({
  providers: [...databaseProviders],
  exports: [...databaseProviders],
})
export class DatabaseModule {}
```

创建用户实体，新建 `user.entity.ts` 文件

```typescript
import { Entity, Column, PrimaryGeneratedColumn } from 'typeorm';

@Entity('user')
export class UserEntity {
  @PrimaryGeneratedColumn()
  id: number;

  @Column({ length: 500 })
  name: string;
}
```

新建 `repository.providers.ts` 文件。

```typescript
import { UserEntity } from './user.entity';

export const repositoryProviders = [
  {
    provide: 'USER_REPOSITORY',
    useFactory: (connection: Connection) =>
      connection.getRepository(UserEntity),
    inject: ['DATABASE_CONNECTION'],
  },
];
```

在模块中导入数据库提供程序和 `repositoryProvides`。

```typescript
import { Module } from '@nestjs/common';
import { DatabaseModule } from '../db/db.module';
import { UserService } from './user.service';

@Module({
  imports: [DatabaseModule],
  providers: [
    ...repositoryProviders,
    UserService,
  ],
})
export class UserModule {}
```

在 `UserService` 中使用 `UserRepository`。

```typescript
import { Injectable, Inject } from '@nestjs/common';
import { Repository } from 'typeorm';
import { UserEntity } from './user.entity';

@Injectable()
export class UserService {
  constructor(
    @Inject('USER_REPOSITORY')
    private userRepository: Repository<UserEntity>,
  ) {}

  async findAll(): Promise<UserEntity[]> {
    return this.userRepository.find();
  }
}
```
