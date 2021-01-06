# 控制器异常处理过滤器（ControllerExceptionFilter）

> `Semple1` 对异常做了处理输出，`Semple2` 集成了国际化（I18N）。

```csharp
[Route("api/[controller]")]
[ApiController]
[ControllerExceptionFilter]
public class ValuesController : ControllerBase
{
    [HttpGet]
    public ActionResult<string[]> Get()
    {
        string str;
        throw new SharedException(nameof(Str), "接收到的参数错误。");
    }
}
```

输出结果

```
Respones:
{
    "title": "参数校验失败，请确保是否有效。",
    "status": 400,
    "instance": "/api/values",
    "errors": {
        "simple": [
            "接收到的参数错误。"
        ]
    }
}
```
