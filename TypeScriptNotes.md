# Table of contents

- [TypeScript Notes](#typescript-notes)
  - [策略模式消除 if-else 的方法](策略模式消除if-else的方法)

# TypeScript Notes

## 策略模式消除 if-else 的方法

```ts
private readonly relationRecord: Record<string, (trashObject: TrashObject) => Promise<IHasDataState[]>> = {};

constructor() {
  this.initRelationRecord();
}

private initRelationRecord(): void {
  this.relationRecord[CategoryEntity.name] = async (trashObject) => {
    const subObjects: IHasDataState[] = [];

    const category = await this.categoryRepository.findOne(trashObject.objectId);

    if (!category) {
      return subObjects;
    }

    const subCategories = await this.categoryRepository.findDescendants(category);
    subObjects.push(subCategories);

    const categoryIds = subCategories.map((p) => p.id);
    const files = await this.fileRepository.find({ where: { parentId: In(categoryIds) } });
    subObjects.push(files);

    return subObjects;
  };

  this.relationRecord[ProjectEntity.name] = async (trashObject) => {
    const subObjects: IHasDataState[] = [];

    const project = await this.projectRepository.findOne(trashObject.objectId);

    if (!project) {
      return subObjects;
    }
    
    const categories = await this.categoryRepository.findOne({ where: { project: trashObject.objectId } });
    subObjects.push(categories);

    const files = await this.fileRepository.findOne({ where: { project: trashObject.objectId } });
    subObjects.push(files);

    return subObjects;
  };
}

private async cascadeDelete(trashObject: TrashObject): Promise<void> {
  const relations = await this.relationRecord[trashObject.objectType](trashObject);

  for (let i = 0; i < relations.length; i++) {
    await this.cascadeDeleteCore(relations[i]);
  }
}
```
