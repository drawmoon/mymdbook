# EF Core 值转换

实体类型如下：

```c#
public class Table
{
    public string Schema { get; set; } // 在数据库中该列的类型是整型
}
```

重写`OnModelCreating`方法，在方法中定义转换。

```c#
modelBuilder.Entity<Table>().Property(e => e.Schema).HasConversion<int>();
```

## 从属实体类型的值转换

从属实体如下：

```c#
[Owned]
public class TableDisplay
{
    public string DisplayId { get; set; } // 在数据库中该列的类型是整型
}
```

所有者实体如下：

```c#
public class Table
{
    public virtual TableDisplay TableDisplay { get; set; }
}
```

在`OnModelCreating`方法中定义转换。

```c#
modelBuilder.Entity<Table>()
    .OwnsOne(e => e.TableFisplay, o => 
    {
        o.Property(tf => tf.DisplayId)
            .HasConversion<int>();
    });
```

## 自动递增主键的值转换

实体类型如下：

```c#
public class Table
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; } // 在数据库中该列的类型是整型
}
```

如果对自动递增主键使用内置转换器将不会起效，`Entity Framework Core`会抛出如下异常：

> Value generation is not supported for property 'Table.Id' because it has a 'StringToNumberConverter<int>' converter configured. Configure the property to not use value generation using 'ValueGenerated.Never' or 'DatabaseGeneratedOption.None' and specify explict values instead.

解决此问题可以尝试自定义转换器和值生成器。

```c#
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

然后在`OnModelCreating`方法中使用该转换器：

```c#
modelBuilder.Entity<Table>()
    .Property(e => e.Id)
    .HasConversion(new StringToIntForIdentityColumnConverter());
```
