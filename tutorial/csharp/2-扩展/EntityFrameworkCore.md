# Hey EntityFramework Core

- [值转换](#值转换)
- [提交更改时为列动态生成值](#提交更改时为列动态生成值)

## 值转换

实体类型如下：

```csharp
public class Table
{
    public string Schema { get; set; } // 在数据库中该列的类型是整型
}
```

重写 `OnModelCreating` 方法，在方法中定义转换。

```csharp
modelBuilder.Entity<Table>().Property(e => e.Schema).HasConversion<int>();
```

### 从属实体类型的值转换

从属实体如下：

```csharp
[Owned]
public class TableDisplay
{
    public string DisplayId { get; set; } // 在数据库中该列的类型是整型
}
```

所有者实体如下：

```csharp
public class Table
{
    public virtual TableDisplay TableDisplay { get; set; }
}
```

在 `OnModelCreating` 方法中定义转换。

```csharp
modelBuilder.Entity<Table>()
    .OwnsOne(e => e.TableFisplay, o => 
    {
        o.Property(tf => tf.DisplayId)
            .HasConversion<int>();
    });
```

### 自动递增主键的值转换

实体类型如下：

```csharp
public class Table
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; } // 在数据库中该列的类型是整型
}
```

如果对自动递增主键使用内置转换器将不会起效，`Entity Framework Core` 会抛出如下异常：

> Value generation is not supported for property 'Table.Id' because it has a 'StringToNumberConverter<int>' converter configured. Configure the property to not use value generation using 'ValueGenerated.Never' or 'DatabaseGeneratedOption.None' and specify explict values instead.

解决此问题可以尝试自定义转换器和值生成器。

```csharp
public class StringToIntForIdentityColumnConverter : StringToNumberConverter<int>
{
    public StringToIntForIdentityColumnConverter() : base(new ConverterMappingHints(valueGeneratorFactory: (p, t) => new IntStringGenerator()))
}

public class IntStringGenerator : ValueGenerator<string>
{
    private int _current = -2147482648;

    public override string Next(EntityEntry entry) => Interlocked.Increment(ref _current).ToString();

    public override bool GeneratesTemporaryValues = true;
}
```

然后在 `OnModelCreating` 方法中使用该转换器：

```csharp
modelBuilder.Entity<Table>()
    .Property(e => e.Id)
    .HasConversion(new StringToIntForIdentityColumnConverter());
```

## 提交更改时为列动态生成值

以下示例在插入或修改实体时动态更新 `CreateTime` 和 `LastModified` 列的值。

重写 `InternalClrEntityEntry`：

```csharp
public class InternalClrEntityEntryOverride : InternalClrEntityEntry
{
    public InternalClrEntityEntryOverride(
        [NotNull] IStateManager stateManager,
        [NotNull] IEntityType entityType,
        [NotNull] object entity) : base(stateManager, entityType, entity)
    {
    }

    public override void SetEntityState(EntityState entityState, bool acceptChanges = false, EntityState? forceStateWhenUnknownKey = null)
    {
        Tweaks(entityState, Entity);
        base.SetEntityState(entityState, acceptChanges, forceStateWhenUnknownKey);
    }

    public override Task SetEntityStateAsync(EntityState entityState, bool acceptChanges, EntityState? forceStateWhenUnknownKey,
        CancellationToken cancellationToken = new CancellationToken())
    {
        Tweaks(entityState, Entity);
        return base.SetEntityStateAsync(entityState, acceptChanges, forceStateWhenUnknownKey, cancellationToken);
    }

    private static void Tweaks(object entity)
    {
        if (entityState != EntityState.Added && entityState != EntityState.Modified)
        {
            return;
        }

        if (entity is IHasCreateTime createTimeEntity && createTimeEntity.CreateTime == DateTime.MinValue)
        {
            createTimeEntity.CreateTime = DateTime.Now;
        }

        if (entity is IHasLastModified lastModifiedEntity)
        {
            lastModifiedEntity.LastModified = DateTime.Now;
        }
    }
}
```

重写 `InternalEntityEntryFactory`：

```csharp
public class InternalEntityEntryFactoryOverride : InternalEntityEntryFactory
{
    public override InternalEntityEntry Create(IStateManager stateManager, IEntityType entityType, object entity)
    {
        return NewInternalEntityEntry(stateManager, entityType, entity);
    }

    public override InternalEntityEntry Create(IStateManager stateManager, IEntityType entityType, object entity,
        in ValueBuffer valueBuffer)
    {
        return NewInternalEntityEntry(stateManager, entityType, entity, in valueBuffer);
    }

    private static InternalEntityEntry NewInternalEntityEntry(
        IStateManager stateManager,
        IEntityType entityType,
        object entity)
    {
        if (!entityType.HasClrType())
            return (InternalEntityEntry)new InternalShadowEntityEntry(stateManager, entityType);
        if (entityType.ShadowPropertyCount() <= 0)
            return (InternalEntityEntry)new InternalClrEntityEntryOverride(stateManager, entityType, entity); // 在此处构建并返回一个 InternalClrEntityEntryOverride 实例
        return (InternalEntityEntry)new InternalMixedEntityEntry(stateManager, entityType, entity);
    }

    private static InternalEntityEntry NewInternalEntityEntry(
        IStateManager stateManager,
        IEntityType entityType,
        object entity,
        in ValueBuffer valueBuffer)
    {
        if (!entityType.HasClrType())
            return (InternalEntityEntry)new InternalShadowEntityEntry(stateManager, entityType, in valueBuffer);
        if (entityType.ShadowPropertyCount() <= 0)
            return (InternalEntityEntry)new InternalClrEntityEntryOverride(stateManager, entityType, entity); // 在此处构建并返回一个 InternalClrEntityEntryOverride 实例
        return (InternalEntityEntry)new InternalMixedEntityEntry(stateManager, entityType, entity, in valueBuffer);
    }
}
```

配置 `Startup`，将 `InternalEntityEntryFactory` 服务替换为 `InternalEntityEntryFactoryOverride`：

```csharp
class Startup {
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<MyDbContext>((sp, options) =>
        {
            options.ReplaceService<IInternalEntityEntryFactory, InternalEntityEntryFactoryOverride>();
        });
    }
}
```