// 使用正则表达式匹配指定字符串

// 返回一个 Boolean 值，指示该字符串是否匹配成功
const result = /,/gm.test('hello, world');
console.log(result); // true

// 对指定字符串进行搜索，返回搜索结果的数组
const pattern1 = /a+/gm;

let arr;
while ((arr = pattern1.exec('a,aaaaaaaabc')) != null) {
  console.log(arr);
}
// ["a", index: 0, input: "a,aaaaaaaabc", groups: undefined]
// [ 'aaaaaaaa', index: 2, input: 'a,aaaaaaaabc', groups: undefined ]


// 将匹配到的分组的值替换为新的值
const pattern2 = /<[^>]*=(?<stain>3D)+\"\S+\"\s*\/?>/g;

const html = '<figure class=3D"image"><img src=3D"file:///C:/fake/image0.png"> <figcaption>3D图形</figcaption>'.replace(pattern2, (sub, stain) => {
  return sub.replace(stain, '');
});
console.log(html); // <figure class="image"><img src="file:///C:/fake/image0.png"> <figcaption>3D图形</figcaption>
