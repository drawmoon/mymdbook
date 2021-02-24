# Table of contents

- [Nest Swagger Notes](#nest-swagger-notes)
  - [Swagger 指定 Query 参数为数组](#swagger-指定-query-参数为数组)
  - [Swagger 指定 form-data 参数为数组](#swagger-指定-form-data-参数为数组)

# Nest Swagger Notes

## Swagger 指定 Query 参数为数组

```ts
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

## Swagger 指定 form-data 参数为数组

```ts
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
