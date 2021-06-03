# EF Core 从属实体类型的值转换

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
