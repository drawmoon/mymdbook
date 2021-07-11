# 策略模式消除 if-else 的方法

```typescript
export class TrashService {
    private readonly relationRecord: Record<string, <TKey>(id: TKey) => Promise<TargetWithRelation>> = {};

    constructor() {
    this.initRelationRecord();
    }

    private initRelationRecord(): void {
    // 处理 Folder 的业务逻辑
    this.relationRecord[FolderEntity.name] = async (id) => {
        // ...
        return targetObject;
    };

    // 处理 File 的业务逻辑
    this.relationRecord[FileEntity.name] = async (id) => {
        // ...
        return targetObject;
    };
    }

    async delete(trashObject: TrashObject): Promise<void> {
    const targetWithRelation = await this.relationRecord[trashObject.objectType](trashObject.objectId);
    // ...
    }
}
```
