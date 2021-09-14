const someDict: Record<string, string> = {
  ['a']: '1',
  ['b']: '2',
  ['c']: '3',
};

console.log(someDict);

// 获取字典的键和值
console.log(Object.keys(someDict));
console.log(Object.values(someDict));

// 更改指定键的值
someDict['a'] = '100';
console.log(someDict);

// 删除字典指定索引处的元素
delete someDict[0];
console.log(someDict);

// 键值对映射
const someMap = new Map();
someMap.set('a', '1');
someMap.set('b', '2');
someMap.set('c', '3');

console.log(someMap);
console.log(someMap.keys());
console.log(someMap.values());

for (const [key, val] of someMap) {
  console.log(`KEY: ${key}, VAL: ${val}`);
}
