# Table of contents

- [NestJs Notes](#nestjs-notes)
  - [x-www-form-urlencode 风格的接口，接收来自 form-data 的参数](#x-www-form-urlencode-风格的接口接收来自-form-data-的参数)
  - [swagger 接口调试，接收来自 form-data 的数组参数](#swagger-接口调试接收来自-form-data-的数组参数)

# NestJs Notes

## x-www-form-urlencode 风格的接口，接收来自 form-data 的参数

## swagger 接口调试，接收来自 form-data 的数组参数

显示声明`property`的`name`，加上`[]`。这样`swagger`发送的是数组，否则是`,`拼接的字符串。

```ts
export class CategoryRestoreDto {
    @ApiProperty({ description: 'restored id array', name: 'ids[]', type: Number, required: true, isArray: true })
    ids : number[];
}
```
