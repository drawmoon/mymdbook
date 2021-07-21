# 策略模式消除 if-else 的方法

```typescript
remove<T>(type: string, id: T) {
  if (type === 'file') {

  } else if (type === 'folder') {

  } else if (type === 'tag') {
    
  } else {
    
  }
}
```

```typescript
private readonly relationRecord: Record<string, <TKey>(id: TKey) => Promise<void>> = {};
  
constructor() {
  this.initRelationRecord();
}

private initRelationRecord(): void {
  this.relationRecord['folder'] = async (id) => {
    
  };

  this.relationRecord['file'] = async (id) => {
    
  };

  this.relationRecord['tag'] = async (id) => {
    
  };
}

async remove<T>(type: string, id: T): Promise<void> {
  await this.relationRecord[type](id);
}
```
