# Table of contents

- [TypeScript Notes](#typescript-notes)
  - [AutoMapper 对象与对象自动映射](#automapper-对象与对象自动映射)
  - [委托](#委托)
  - [策略模式消除 if-else 的方法](策略模式消除if-else的方法)

# TypeScript Notes

## AutoMapper 对象与对象自动映射

安装依赖项

```bash
npm i @automapper/core @automapper/classes reflect-metadata
npm i -D @automapper/types
```

用`AutoMap`装饰属性

```ts
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

创建`Profile`

```ts
// mapper-profile.ts
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

创建`Mapper`

```ts
// mapper.ts
import { classes } from "@automapper/classes";
import { createMapper } from "@automapper/core";
import { UserProfile } from "./mapper-profile";

export const Mapper = createMapper({
  name: 'userMapper',
  pluginInitializer: classes,
});

Mapper.addProfile(UserProfile);
```

使用`Mapper`

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

## 委托

```ts
async convertToHtml(buffer: Buffer, convertImgCallback: (imgBase64Buffer: Buffer) => Promise<string>): Promise<string> {
  // ...
  for (const img of doc.images) {
    const imgBase64Buffer = img.read('base64');
    img.src = await convertImgCallback(imgBase64Buffer);
  }
  // ...
}

async importDocx(buffer: Buffer): Promise<void> {
  await convertToHtml(buffer, async (imgBase64Buffer) => {
    await fs.writeFile(imgPath, imgBase64Buffer, 'base64');
    return imgPath;
  });
  // ...
}

```

## 策略模式消除 if-else 的方法

```ts
private readonly relationRecord: Record<string, <TKey>(id: TKey) => Promise<TargetWithRelation>> = {};

constructor() {
  this.initRelationRecord();
}

private initRelationRecord(): void {
  // 处理 Category
  this.relationRecord[CategoryEntity.name] = async (id) => {
    // ...
    return targetObject;
  };

  // 处理 File
  this.relationRecord[FileEntity.name] = async (id) => {
    // ...
    return subObjects;
  };
}

async delete(trashObject: TrashObject, entityManager: EntityManager): Promise<void> {
  const targetWithRelation = await this.relationRecord[trashObject.objectType](trashObject.objectId);
  // ...
}
```

<details>
  <summary>完整代码</summary>

```ts
private readonly relationRecord: Record<string, <TKey>(id: TKey) => Promise<TargetWithRelation>> = {};

constructor() {
  this.initRelationRecord();
}

private initRelationRecord(): void {
  this.relationRecord[CategoryEntity.name] = async (id) => {
    const targetObject: TargetWithRelation = {};
    const subObjects: ICanTrash[] = [];

    const category = await this.categoryRepository.findOne(id);

    if (!category) {
      return targetObject;
    }

    const categoryIds = (await this.categoryRepository.findDescendants(category)).map((p) => p.id);
    const files = await this.fileRepository.find({ where: { parentId: In(categoryIds) } });
    subObjects.push(...files);

    const subCategoryTree = await this.categoryRepository.findDescendantsTree(category);
    // 在里面处理好层级关系，从下往上读取，将子级目录添加到数组的前面
    const subCategories = await this.getCategoryRelationSubObjects(subCategoryTree);
    for (const subCategory of subCategories) {
      if (subCategory.id != category.id) {
        subObjects.push(subCategory);
      }
    }

    targetObject.target = category;
    targetObject.relations = subObjects;
    return targetObject;
  };

  this.relationRecord[FileEntity.name] = async (id) => {
    const targetObject: TargetWithRelation = {};
    const subObjects: ICanTrash[] = [];

    const file = await this.fileRepository.findOne(id);

    if (!file) {
      return subObjects;
    }

    targetObject.target = file;
    targetObject.relations = subObjects;
    return subObjects;
  };
}

private async delete(trashObject: TrashObject, entityManager: EntityManager): Promise<void> {
  const targetWithRelation = await this.relationRecord[trashObject.objectType](trashObject.objectId);

  const target = targetWithRelation.target;
  switch (target.state) {
    case DataState.Normal:
      await this.cascadeDelete(targetWithRelation.relations);
      target.state = DataState.Deleted;
      await entityManager.save(target);
      break;
    case DataState.Deleted:
      await this.cascadeHardDelete(targetWithRelation.relations);
      await entityManager.remove(target);
      break;
    case DataState.CascadeDeleted:
      // 不处理
      break;
  }
}
```

</details>
