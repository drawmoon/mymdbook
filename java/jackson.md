# Jackson

## 自定义序列化与反序列化

### JsonAnyGetter

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

序列化输出的 JSON:

```json
{
    "key1": "value1",
    // ...
    "total": 10
}
```

### JsonAnySetter

```java
import java.util.HashMap;
import java.util.Map;

import com.fasterxml.jackson.annotation.JsonAnySetter;

public class MyClass {

    private Map<String, List<String>> map = new HashMap<>();

    @JsonAnySetter
    private void setProperty(String key, List<String> value) {
        this.map.put(key, value);
    }
}
```

反序列化输入的 JSON:

```json
{
    "array": ["a", "b", "c"]
}
```

执行反序列化会将 `array` 作为键添加 `Map` 中。
