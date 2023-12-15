/*
  异步模型
 */
// Callback 形式的异步模型
function readFile(path: string, callback :(err: Error, data: Buffer) => void): void {
  setTimeout(() => {
    const data = Buffer.from('123456abcdefg', 'utf-8');
    callback(undefined, data);
  }, 2000);
}

readFile('assets/text.txt', (err, data) => {
  if (err) {
    console.error(err.message);
  } else {
    console.log(data.toString('utf-8'));
  }
});

// Promise 形式的异步模型
function readFileAsync(path: string): Promise<Buffer> {
  return new Promise<Buffer>((resolve, reject) => {
    setTimeout(() => {
      const data = Buffer.from('123456abcdefg', 'utf-8');
      resolve(data);
    }, 2000);
  });
}

readFileAsync('assets/text.txt')
  .then((data) => {
    console.log(data.toString('utf-8'));
  })
  .catch((err) => {
    console.error(err.message);
  });

console.log('这句代码在异步函数之前被执行');

/*
  将函数转为后台执行
 */
async function mainAsync(): Promise<number> {
  const save = async () => {
    await delay(2000);
    console.log('save table with id = 1.');
  };

  const publish = async () => {
    await delay(5000);
    console.log('publish table with id = 1.');
  };

  const notify = async () => {
    await delay(2000);
    console.log('notify the table with id = 1 has been published.');
  };

  await save();

  // functions will execute in background
  publish().then();
  notify().then();

  // or use `Promise.all`
  // Promise.all([publish(), notify()]).then();

  console.log('successful.');
  return 1;
}

function delay(ms = 1): Promise<void> {
  return new Promise<void>((r) => {
    setTimeout(r, ms);
  });
}

mainAsync().then((id) => {
  console.log(`id = ${id}`);
});

/*
  返回异步结果
 */
function canView(): Promise<boolean> {
  return Promise.resolve(true);
}

canView().then((accept) => {
  console.log(accept);
});
