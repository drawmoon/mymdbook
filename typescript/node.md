# Node

- [HTTP 请求](#http-请求)

## HTTP 请求

### Axios

#### MultipartFile

```bash
npm install form-data
```

```typescript
import * as FormData from 'form-data';

const form = new FormData();
form.append('name', '新建文件');
form.append('file', createReadStream('/path/to/simple.txt'));

await axios.post('/api/file/upload', form, {
  baseURL: 'http://localhost:3000',
  headers: { ...form.getHeaders() },
});
```
