# TypeScript Notes

- [委托](#委托)
- [策略模式消除 if-else 的方法](策略模式消除if-else的方法)

## 委托

```ts
async convertToHtml(buffer: Buffer, imageConvert: (imgBase64Buffer: Buffer) => Promise<string>): Promise<string> {
  // ...
  for (const img of doc.images) {
    const imgBase64Buffer = img.read('base64');
    img.src = await imageConvert(imgBase64Buffer);
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
    return targetObject;
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
      return targetObject;
    }

    targetObject.target = file;
    targetObject.relations = subObjects;
    return targetObject;
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
