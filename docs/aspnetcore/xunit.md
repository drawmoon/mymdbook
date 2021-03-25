# xUnit Notes

- [测试两个集合的元素是否相等](#判断两个集合的元素是否相等)
- [测试异常情况](#测试异常情况)

## 测试两个集合的元素是否相等

```csharp
List<string> foo = new(){ "A", "B" };
List<string> bar = new(){ "A" };

// 是否全部包含
Assert.All(foo, p => Assert.Contains(p, bar));

// 是否全部不包含
Assert.All(foo, p => Assert.DoseNotContains(p, bar));
```

## 测试异常情况

```csharp
var exception = await Assert.ThrowsAsync<AppException>(async () =>
{
    await tableFieldService.Update(filed);
});

Assert.Equal("存在重复的表字段名称", exception.Message);
Assert.Equal(200400, exception.ErrorCode);
```
