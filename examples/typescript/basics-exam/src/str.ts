/*
  是否以指定字符串开始、结束
 */
const timeStr = '1 day ago';
console.log(timeStr.startsWith('1'), timeStr.endsWith('ago'));

/*
  是否包含指定字符串
 */
console.log('1 week ago'.includes('week'));

/*
  将字符串拆分为数组
 */
console.log('Hello,World'.split(','));

/*
  提取字符串的一部分并返回一个新字符串
 */
const someStr = 'Hello, World!';

// 返回指定开始索引到结束索引之间的字符串，索引支持负数
console.log(someStr.slice()); // Hello, World!
console.log(someStr.slice(0, 5)); // Hello
console.log(someStr.slice(0, -8)); // Hello
console.log(someStr.slice(7)); // World!

// 返回指定开始索引到结束索引之间的字符串
console.log(someStr.substring(0, 5)); // Hello
console.log(someStr.substring(7)); // World!

// 返回指定开始索引到指定长度之间的字符串
console.log(someStr.substr(0, 5)); // Hello
console.log(someStr.substr(7)); // World!

/*
  返回指定字符串的索引
 */
const someStr2 = 'Hello, TypeScript, World!';

console.log(someStr2.indexOf(','));
console.log(someStr2.lastIndexOf(','));

/*
  返回指定索引处的字符
 */
const someStr3 = 'Hello, World!';

console.log(someStr3.charAt(5));

/*
  将字符串转换为大写、小写
 */
console.log('TypeScript'.toUpperCase(), 'TypeScript'.toLowerCase());

/*
  format
 */
function strformat(str: string, ...args: string[]) {
  for (const k in args) {
    str = str.replace(`{${k}}`, args[k]);
  }
  return str;
}

console.log(strformat('Hello, {0}!', 'World'));
