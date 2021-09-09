const arr = [1, 2, 3];
console.log(arr[3]); // undefined

// 拼接数组中的元素为字符串
const sb: string[] = [];
console.log(sb.join(' and ')); // ''

// 提取指定开始索引到结束索引之间的元素
const arr2 = new Array(6);
arr2[0] = 'George';
arr2[1] = 'John';
arr2[2] = 'Thomas';
arr2[3] = 'James';
arr2[4] = 'Adrew';
arr2[5] = 'Martin';

console.log(arr2.slice(0, 3)); // [ 'George', 'John', 'Thomas' ]
console.log(arr2.slice(0, -2)); // [ 'George', 'John', 'Thomas', 'James' ]
