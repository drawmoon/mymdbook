# 支持多模式的密码认证处理器（PasswordAuthorizationFilter）

```csharp
// GET
[PasswordAuth("Paw1")] // 使用 Paw1 认证模式
[HttpGet("simple1")]
public ActionResult<string[]> Get()
{
    return new[] {"value1", "value2"};
}

// GET
[PasswordAuth("Paw2")] // 使用 Paw2 认证模式
[HttpGet("simple2")]
public ActionResult<string[]> Get2()
{
    return new[] {"value1", "value2"};
}
```

输出结果

```
Headers-------------------------
✔ X-Pwd  admin
--------------------------------
Respones:
[
  "value1",
  "value2"
]



Headers-------------------------
❌ X-Pwd  admin
--------------------------------
Respones:
{
    "type": "https://tools.ietf.org/html/rfc7235#section-3.1",
    "title": "Unauthorized",
    "status": 401,
    "traceId": "|2dc8fc8e-4f0bfd54f9f355ea."
}
```
