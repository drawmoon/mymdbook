# Nest Swagger

- [配置启用 Swagger](#配置启用-swagger)
- [描述接口参数](#描述接口参数)
  - [数组](#数组)
- [File upload](#file-upload)
  - [Single file](#single-file)
  - [Any files](#any-files)
  - [Multiple files](#multiple-files)

## 配置启用 Swagger

安装依赖项

```bash
npm install --save @nestjs/swagger swagger-ui-express
```

在 `main.ts` 中初始化 `swagger`

```typescript
async function bootstrap() {
  const app = await NestFactory.create(AppModule);

  // 初始化 Swagger
  const config = new DocumentBuilder()
    .setTitle('ToDo API')
    .setDescription('A simple example nodejs Web API')
    .setVersion('v1')
    .build();
  const document = SwaggerModule.createDocument(app, config);
  SwaggerModule.setup('api', app, document);

  await app.listen(3000);
}
bootstrap();
```

在 `controller` 中添加控制器描述文本

```typescript
import { ApiTags } from '@nestjs/swagger';

@ApiTags('ToDo')
export class TodoController {}
```

## 描述接口参数

## 数组

Query 参数：

```typescript
@ApiQuery({
  name: 'id',
  description: 'The param description',
  type: Number,
  isArray: true,
})
getById(
  @Query(
    'id',
    new ParseArrayPipe({
      items: Number,
      optional: false,
    })
  )
  id: number[]
)
```

Body 参数：

```typescript
class GetUserByIdDTO {
  @ApiProperty({
    description: 'The param description',
    type: Number,
    isArray: true,
  })
  id: number[];
}

@ApiBody({
  type: GetUserByIdDTO,
})
getById(
  @Body(
    'id',
    new ParseArrayPipe({
      items: Number,
      optional: false,
    })
  )
  ids: number[]
)
```

## File upload

安装依赖

```bash
npm install -D @types/multer
```

### Single file

```typescript
@ApiConsumes('multipart/form-data')
@ApiBody({
  schema: {
    type: 'object',
    required: ['file'],
    properties: {
      file: {
        description: '文件的二进制数据',
        type: 'file',
        format: 'binary',
      },
    },
  },
})
@UseInterceptors(FileInterceptor('file'))
upload(@UploadedFile() file: Express.Multer.File)
```

### Any files

```typescript
@ApiConsumes('multipart/form-data')
@ApiBody({
  schema: {
    type: object,
    required: ['files'],
    properties: {
      ['files']: {
        type: 'array',
        description: '文件的二进制数据',
        items: {
          type: 'file',
          format: 'binary',
        },
      },
    },
  },
})
@UseInterceptors(AnyFilesInterceptor())
upload(@UploadedFiles() files: Express.Multer.File[])
```

### Multiple files

```typescript
@ApiConsumes('multipart/form-data')
@ApiBody({
  schema: {
    type: object,
    required: ['index.docx'],
    properties: {
      ['readme.md']: {
        description: 'Readme.md 二进制数据',
        type: 'file',
        format: 'binary',
        nullable: false,
      },
      ['index.docx']: {
        description: 'Index.docx 二进制数据',
        type: 'file',
        format: 'binary',
        nullable: true,
      },
    },
  },
})
@UseInterceptors(
  FileFieldsInterceptor([
    { name: 'readme.md', maxCount: 1 },
    { name: 'index.docx', maxCount: 1 },
  ])
)
upload(
  @UploadedFile('readme.md') readme: Express.Multer.File,
  @UploadedFile('index.docx') docx: Express.Multer.File,
)
```
