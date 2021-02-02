# 在 ASP.NET Core 中应用 json-patch

## Operations

### Replace

```json
{ "op": "replace", "path": "/name", "value": "myOrder" }
```

替换子对象的属性的值

```json
{ "op": "replace", "path": "/orderDetails/name", "value": "珍珠奶茶B" }
```

替换子对象的值

```json
{ "op": "replace", "path": "/orderDetails", "value": [ { "Name": "珍珠奶茶B" } ] }
```
