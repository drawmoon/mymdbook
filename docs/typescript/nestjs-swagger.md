# Nest Swagger Notes

- [初始化 Swagger](#初始化-swagger)
- [描述接口参数](#描述接口参数)
  - [数组类型的 query 参数](#数组类型的-query-参数)
  - [数组的 form-data 参数](#数组的-form-data-参数)
- [文件上传](#文件上传)
  - [上传单个文件](#上传单个文件)
  - [上传多个文件](#上传多个文件)
  - [上传指定文件](#上传指定文件)

## 初始化 Swagger

安装依赖项

```bash
npm install --save @nestjs/swagger swagger-ui-express
```

在`main.ts`中初始化`swagger`

```typescript
async function bootstrap() {
  const app = await NestFactory.create(AppModule);

  // 初始化 Swagger
  const config = new DocumentBuilder()
    .setTitle("ToDo API")
    .setDescription("A simple example nodejs Web API")
    .setVersion("v1")
    .build();
  const document = SwaggerModule.createDocument(app, config);
  SwaggerModule.setup("api", app, document);

  await app.listen(3000);
}
bootstrap();
```

在`controller`中添加控制器描述文本

```typescript
import { ApiTags } from "@nestjs/swagger";

@ApiTags("ToDo")
export class TodoController {}
```

## 描述接口参数

## 数组类型的 query 参数

```typescript
export class TodoController {
  @Delete()
  @ApiQuery({
    name: "ids",
    description: "The param description",
    type: Number,
    isArray: true,
    required: true,
  })
  batchDelete(
    @Query(
      "ids",
      new ParseArrayPipe({
        items: Number,
      })
    )
    ids: number[]
  ): FolderDTO[] {}
}
```

## 数组的 form-data 参数

```typescript
export class UserBatchDeleteDto {
  @ApiProperty({
    description: "The param description",
    type: Number,
    isArray: true,
    required: true,
  })
  ids: number[];
}

export class TodoController {
  @Delete()
  @ApiBody({
    type: UserBatchDeleteDto,
  })
  batchDelete(
    @Body(
      "ids",
      new ParseArrayPipe({
        items: Number,
      })
    )
    ids: number[]
  ): FolderDTO[] {}
}
```

## 文件上传

安装依赖

```bash
npm install -D @types/multer
```

### 上传单个文件

```typescript
export class TodoController {
  @ApiPost("upload")
  @ApiBody({
    description: "上传的文件",
    schema: {
      type: "object",
      properties: {
        file: {
          type: "file",
          format: "binary",
        },
      },
    },
  })
  @ApiConsumes("multipart/form-data")
  @UseInterceptors(FileInterceptor("file"))
  upload(@UploadedFile() file: Express.Multer.File): void {}
}
```

### 上传多个文件

```typescript
export class TodoController {
  @ApiPost("upload")
  @ApiBody({
    description: "上传的文件",
    schema: {
      type: object,
      properties: {
        ["files"]: {
          type: "array",
          items: {
            type: "file",
            format: "binary",
          },
        },
      },
    },
  })
  @ApiConsumes("multipart/form-data")
  @UseInterceptors(AnyFilesInterceptor())
  upload(): void {}
}
```

### 上传指定文件

```typescript
export class TodoController {
  @ApiPost("upload")
  @ApiBody({
    description: "上传的文件",
    schema: {
      type: object,
      properties: {
        ["readme.md"]: {
          type: "file",
          format: "binary",
        },
        ["index.docx"]: {
          type: "file",
          format: "binary",
        },
      },
    },
  })
  @ApiConsumes("multipart/form-data")
  @UseInterceptors(
    FileFieldsInterceptor([
      { name: "readme.md", maxCount: 1 },
      { name: "index.docx", maxCount: 1 },
    ])
  )
  upload(
    @UploadedFile("readme.md") readme: Express.Multer.File,
    @UploadedFile("index.docx") docx: Express.Multer.File
  ): void {}
}
```
