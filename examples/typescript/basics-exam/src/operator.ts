/*
    && 运算符，左侧值结果为 true 时，则返回右侧值，否则返回左侧值
 */

const names: string[] = undefined;
console.log(names && names[0]);
const names2: string[] = ['Loren Dean'];
console.log(names2 && names2[0]);
const success = false;
console.log(success && 1 + 2);

/*
    || 运算符，左侧值结果为 false 时，则返回右侧值。判断结果是否为 false 不仅包括 undefined 和 null，还包括 false、 '' 和 0
 */

const inputs = [undefined, null, '', 0, false, 'value'];
for (const input of inputs) {
  console.log(input || 'abc');
}

/*
    ?? 运算符，左侧值结果为 false 时，则返回右侧值。判断结果是否为 false 仅包括 undefined 和 null
 */

const inputs2 = [undefined, null, '', 0, false, 'value'];
for (const input of inputs2) {
  console.log(input ?? 'abc');
}
