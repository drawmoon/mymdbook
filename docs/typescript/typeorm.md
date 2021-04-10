# TypeOrm Notes

- [使用 TypeOrm](#使用-typeorm)
- [更新树实体的父级实体时，没有更新mpath的问题临时解决方案](更新树实体的父级实体时没有更新-mpath-的问题临时解决方案)

## 使用 TypeOrm

安装依赖项，下面的数据库是`postgres`

```bash
npm install --save typeorm pg
```

新建`db.providers.ts`文件，与数据库建立连接

```typescript
// db.providers.ts
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

新建`db.module.ts`文件，使数据库提供程序可以被其他模块导入

```typescript
// db.module.ts
import { Module } from '@nestjs/common';

@Module({
  providers: [...databaseProviders],
  exports: [...databaseProviders],
})
export class DatabaseModule {}
```

新建用户实体，

```typescript
// user.entity.ts
import { Entity, Column, PrimaryGeneratedColumn } from 'typeorm';

@Entity('user')
export class UserEntity {
  @PrimaryGeneratedColumn()
  id: number;

  @Column({ length: 500 })
  name: string;
}
```

新建`repository.providers.ts`文件

```typescript
// repository.providers.ts
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

在用户模块中导入数据库提供程序和`repositoryProvides`

```typescript
// user.module.ts
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

在`UserService`中使用`UserRepository`

```typescript
// user.service.ts
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

## 更新树实体的父级实体时，没有更新 mpath 的问题临时解决方案

> ISSUES: [https://github.com/typeorm/typeorm/issues/2418](https://github.com/typeorm/typeorm/issues/2418)

该方法参考自`davran-siv`用户的解决方法：[https://github.com/typeorm/typeorm/issues/2418#issuecomment-658194824](https://github.com/typeorm/typeorm/issues/2418#issuecomment-658194824)

```ts
public static async fixMPath<TKey, TEntity>(
  id: TKey,
  parentId: TKey | null | undefined,
  entityRepository: TreeRepository<TEntity>,
  entityManager?: EntityManager,
): Promise<void> {
  const entity = await entityRepository.findOne(id);
  if (!entity) {
    return;
  }

  const entityMpath = await this.getEntityMpath(entityRepository, id);
  const parentMpath = parentId && (await this.getEntityMpath(entityRepository, parentId));

  const newMpath = `${parentMpath}${id}.`;

  const entityWithChildren = await entityRepository.findDescendants(entity);
  const toUpdate = entityWithChildren.map((p) => p['id']);

  const entityMetadata = entityRepository.metadata;
  const query = `UPDATE ${entityMetadata.tableName} SET mpath = REPLACE(mpath, '${entityMpath}', '${newMpath}')  WHERE ${entityMetadata.tableName}.id IN (${toUpdate.join(',')})`;
  if (entityManager) {
    await entityManager.query(query);
  } else {
    await entityRepository.query(query);
  }
}

private static async getEntityMpath<TKey, TEntity>(entityRepository: TreeRepository<TEntity>, id: TKey): Promise<string> {
  const {
    mpath,
  } = await entityRepository
    .createQueryBuilder()
    .where('id = :id', { id: id })
    .select('mpath')
    .getRawOne();

  if (!mpath) {
    throw new InternalServerErrorException('fix mpath failed,entity or mpath not found.');
  }

  return mpath;
}
```
