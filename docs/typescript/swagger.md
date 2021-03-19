# Table of contents

- [Nest Swagger Notes](#nest-swagger-notes)
  - [描述 query 参数为数组](#描述-query-参数为数组)
  - [描述 form-data 参数为数组](#描述-form-data-参数为数组)
  - [描述文件上传](#描述文件上传)
  - [描述多个文件上传](#描述多个文件上传)
  - [描述指定文件上传](#描述指定文件上传)

# Nest Swagger Notes

## 描述 query 参数为数组

```typescript
// folder.controller.ts
@Delete('batch')
@ApiQuery({
  name: 'ids',
  description: '批量删除的文件夹的Id',
  required: true,
  type: [Number],
  isArray: true,
  style: 'form',
  explode: true,
})
batchDelete(@Query('ids', ParseArrayPipe) ids: number[]): FolderDTO[] {
}
```

## 描述 form-data 参数为数组

```typescript
// folder-batch-delete.dto.ts
@ApiProperty({
  description: '批量删除的文件夹的Id',
  type: Number,
  isArray: true,
  required: true,
})
ids: number[];

// folder.controller.ts
@Delete()
@ApiBody({
  type: UserBatchDeleteDto,
})
batchDelete(@Body('ids', ParseArrayPipe) ids: number[]): FolderDTO[] {
}
```

## 描述文件上传

```typescript
@ApiPost('upload')
@ApiBody({
  description: '`.txt`后缀的文件',
  schema: {
    type: 'object',
    properties: {
      file: {
        type: 'file',
        format: 'binary',
      },
    },
  },
})
@ApiConsumes('multipart/form-data')
@UseInterceptors(FileInterceptor('file'))
upload(@UploadedFile() file: Express.Multer.File): void {
}
```

## 描述多个文件上传

```typescript
@ApiPost('upload')
@ApiBody({
  description: '`.txt`后缀的文件',
  schema: {
    type: object,
    properties: {
      ['files']: {
        type: 'array',
        items: {
          type: 'file',
          format: 'binary',
        },
      },
    },
  },
})
@ApiConsumes('multipart/form-data')
@UseInterceptors(AnyFilesInterceptor())
upload(): void {
}
```

## 描述指定文件上传

```typescript
@ApiPost('upload')
@ApiBody({
  description: '`.txt`后缀的文件',
  schema: {
    type: object,
    properties: {
      ['readme.md']: {
        type: 'file',
        format: 'binary',
      },
      ['index.docx']: {
        type: 'file',
        format: 'binary',
      }
    },
  },
})
@ApiConsumes('multipart/form-data')
@UseInterceptors(
  FileFieldsInterceptor([
    { name: 'readme.md', maxCount: 1 },
    { name: 'index.docx', maxCount: 1 },
  ]),
)
upload(
  @UploadedFile('readme.md') readme: Express.Multer.File,
  @UploadedFile('index.docx') docx: Express.Multer.File
): void {
}
```
