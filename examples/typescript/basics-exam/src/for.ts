const words = ['Hello', 'World'];

/*
  for 语法
 */
for (let i = 0; i < words.length; i++) {
  console.log(i, words[i]);
}

/*
  for-in 语法
 */
for (const wordsKey in words) {
  console.log(wordsKey, words[wordsKey]);
}

/*
  for-of 语法
 */
for (const word of words) {
  console.log(word);
}
