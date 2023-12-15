// 数组的声明
const arr1 = ['Hello', 'World'];
console.log(arr1); // ['Hello', 'World']

const arr2 = new Array(2);
arr2[0] = 'Hello';
arr2[1] = 'World';
console.log(arr2); // [ 'Hello', 'World' ]


// 获取指定索引处的元素；超出范围时返回 undefined
const arrName = ['Abubakr', 'Esparza', 'Veronica'];

console.log(arrName[0]); // Abubakr
console.log(arrName[-1]); // undefined
console.log(arrName[3]); // undefined


// 拼接数组中的元素为字符串
const arrWords = ['Hello', 'World'];

console.log(arrWords.join(', ')); // Hello, World


// 提取指定开始索引到结束索引之间的元素
const arrName2 = ['George', 'John', 'Thomas', 'James', 'Adrew', 'Martin'];

console.log(arrName2.slice(0, 3)); // [ 'George', 'John', 'Thomas' ]
console.log(arrName2.slice(0, -2)); // [ 'George', 'John', 'Thomas', 'James' ]


// 移除数组元素
const arrStr = ['A', 'B', 'C', 'D'];

delete arrStr[1];
console.log(arrStr); // [ 'A', 'C', 'D' ]

// 移除并返回数组的第一个元素
console.log(arrStr.shift()); // A

// 删除并返回数组的最后一个元素
console.log(arrStr.pop()); // D


// 去重
const arrStr2 = [1, 1, 'A', 'A'];
console.log(Array.from(new Set(arrStr2))); // [ 1, 'A' ]


// 排序
const arrAnomalyDigit = [55, 1, 12, 7, 8];

console.log(arrAnomalyDigit.sort((a, b) => a > b ? 1 : -1)); // [ 1, 7, 8, 12, 55 ]
console.log(arrAnomalyDigit.sort((a, b) => a < b ? 1 : -1)); // [ 55, 12, 8, 7, 1 ]


// 反转数组
const arrNum = [1, 2, 3, 4, 5];
console.log(arrNum.reverse());


// 合并、并集、交集、差集
const arrLeft = [1, 2, 3];
const arrRigth = [2, 3, 4];

// 合并
console.log([...arrLeft, ...arrRigth]); // [ 1, 2, 3, 2, 3, 4 ]
// 并集
console.log(Array.from(new Set([...arrLeft, ...arrRigth]))); // [ 1, 2, 3, 4 ]
// 交集
console.log(arrLeft.filter((x) => arrLeft.includes(x))); // [ 1, 2, 3 ]
// 差集
console.log(arrLeft.filter((x) => !arrRigth.includes(x))); // [ 1 ]


// 求和
const arrDigit = [50, 20, 30];
console.log(arrDigit.reduce((a, b) => a + b)); // 100


// 所有元素满足条件
const arrStatus = [true, false, true];
console.log(arrStatus.every((x) => x)); // false
