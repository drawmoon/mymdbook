# EF Core 提交更改时为列动态生成值

以下示例在插入或修改实体时动态更新`CreateTime`和`LastModified`列的值

```c#
// 重写 InternalClrEntityEntry
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

// 重写 InternalEntityEntryFactory
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

在`Startup.cs`中将`InternalEntityEntryFactory`服务替换为`InternalEntityEntryFactoryOverride`

```c#
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
