# Table of contents

- [NestJs Notes](#nestjs-notes)
  - [Swagger Query Parameters](#swagger-数组-query-参数)
  - [x-www-form-urlencode 风格的接口，接收来自 form-data 的参数](#x-www-form-urlencode-风格的接口接收来自-form-data-的参数)
  - [Swagger 描述数组形式的 form-data 参数](#swagger-描述数组形式的-form-data-参数)

# NestJs Notes

## Swagger 描述数组形式的 Query 参数

```ts
@Delete()
@ApiQuery({
  name: 'id',
  description: '批量删除的用户Id',
  required: true,
  type: [Number],
  isArray: true,
  style: 'form',
  explode: true,
})
batchDelete(@Query('id', ParseArrayPipe) id: number[]): UserDTO[] {}
```

## x-www-form-urlencode 风格的接口，接收来自 form-data 的参数

```ts

```

## Swagger 描述数组形式的 form-data 参数

```ts
// user-batch-delete.dto.ts
export class UserBatchDeleteDto {
  @ApiProperty({
    description: '批量删除的用户Id',
    type: Number,
    isArray: true,
    required: true,
  })
  ids: number[];
}

// users.controller.ts
@Delete()
@ApiBody({
  type: UserBatchDeleteDto,
})
batchDelete(@Body('ids', ParseArrayPipe) ids: number[]): UserDTO[] {}
```
