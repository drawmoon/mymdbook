# Table of contents

- [NestJs Notes](#nestjs-notes)
  - [Swagger Query Parameters](#swagger-query-parameters)
  - [x-www-form-urlencode 风格的接口，接收来自 form-data 的参数](#x-www-form-urlencode-风格的接口接收来自-form-data-的参数)
  - [swagger 接口调试，接收来自 form-data 的数组参数](#swagger-接口调试接收来自-form-data-的数组参数)

# NestJs Notes

## Swagger Query Parameters

```ts
@Delete()
@ApiQuery({
  name: 'id',
  description: '',
  required: true,
  type: [Number],
  isArray: true,
  style: 'form',
  explode: true,
})
batchDelete(@Query('id', ParseArrayPipe) id: number[]): UserDto[] {}
```

## x-www-form-urlencode 风格的接口，接收来自 form-data 的参数

## Swagger 接口调试，接收来自 form-data 的数组参数
