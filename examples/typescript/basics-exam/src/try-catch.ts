/*
  try-catch 语法
 */
try {
  throw new Error('报错了!');
} catch (e) {
  console.log(e.message);
}

/*
  try-catch-finally 语法
 */
try {
  throw new Error('报错了!');
} catch (e) {
  console.log(e.message);
} finally {
  console.log('始终执行');
}

/*
  try-finally 语法
 */
try {
  throw new Error('报错了!');
} finally {
  console.log('始终执行');
}
