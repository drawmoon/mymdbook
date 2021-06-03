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
modelBuilder.Entity<Table>()
    .Property(e => e.Schema)
    .HasConversion<int>();
```
