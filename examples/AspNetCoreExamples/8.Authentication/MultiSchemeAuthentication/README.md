# 多模式身份验证

```
GET http://localhost:5000/api/values/v1

Headers-------------------------
✔ X-Password  admin
--------------------------------
Respones:
[
  "value1",
  "value2"
]



GET http://localhost:5000/api/values/v2

Headers-------------------------
❌ X-Password  admin
--------------------------------
Respones:
{
    "type": "https://tools.ietf.org/html/rfc7235#section-3.1",
    "title": "Unauthorized",
    "status": 401,
    "traceId": "|2dc8fc8e-4f0bfd54f9f355ea."
}
```
