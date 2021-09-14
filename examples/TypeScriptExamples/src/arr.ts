const someArr = [1, 2, 3];
console.log(someArr[3]); // undefined

// 拼接数组中的元素为字符串
const someArr2: string[] = [];
console.log(someArr2.join(' and ')); // ''

// 提取指定开始索引到结束索引之间的元素
const someArr3 = new Array(6);
someArr3[0] = 'George';
someArr3[1] = 'John';
someArr3[2] = 'Thomas';
someArr3[3] = 'James';
someArr3[4] = 'Adrew';
someArr3[5] = 'Martin';

console.log(someArr3.slice(0, 3)); // [ 'George', 'John', 'Thomas' ]
console.log(someArr3.slice(0, -2)); // [ 'George', 'John', 'Thomas', 'James' ]

// 删除并返回最后一个元素
console.log(someArr3.pop());

// 合并两个数组
const someArr4 = [1, 2, 3];
const someArr5 = [4, 5, 6];
console.log([...someArr4, ...someArr5]);
