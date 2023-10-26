# Jackson

## 自定义 Map 序列化

- 使用 `@JsonFormat` 只转换声明的字段，忽略 `HashMap` 项。
- 使用 `@JsonAnyGetter` 返回 `HashMap` 项。

```java
import java.util.HashMap;
import java.util.Map;

import com.fasterxml.jackson.annotation.JsonAnyGetter;
import com.fasterxml.jackson.annotation.JsonFormat;
import com.fasterxml.jackson.annotation.JsonProperty;

@JsonFormat(shape = JsonFormat.Shape.OBJECT)
public class CustomMap extends HashMap<String, Object> {

    @JsonAnyGetter
    public Map<String, Object> getProperties() {
        return this;
    }

    @JsonProperty("total")
    public Double getTotalValue() {
        return this.size()
    }
}
```

输出的 JSON:

```json
{
    "key1": "value1",
    // ...
    "total": 10
}
```
