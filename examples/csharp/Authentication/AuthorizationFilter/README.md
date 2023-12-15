# AuthorizationFilter

```
GET http://localhost:5001/api/v1/values

Headers-------------------------
✔ X-Password  admin
--------------------------------
Respones:
[
  "value1",
  "value2"
]



GET http://localhost:5001/api/v2/values

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
