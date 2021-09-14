// 回调函数
function someCallbackFn(): string {
  return runTask((id): string => {
    console.log('doSome');
    return `Call task, id: <<${id}>>`;
  });
}

function runTask(callback: (id: string) => string): string {
  console.log('runTask');
  const taskId = '1';
  return callback(taskId);
}

console.log(someCallbackFn());

// 异步回调函数
async function someCallbackFn2(callback: (id: string) => Promise<string>) {
  const tasks = ['1'];
  for (const task of tasks) {
    const str = await callback(task);
    console.log(str);
  }
}

someCallbackFn2(async (id) => {
  // do something...
  return `Call task, id: <<${id}>>`;
});

// 泛型回调函数
function someCallbackFn3(callback: <T>(id: T) => string) {
  const tasks = ['1'];
  for (const task of tasks) {
    const str = callback(task);
    console.log(str);
  }
}

someCallbackFn3((id) => {
  // do something...
  return `Call task, id: <<${id}>>`;
});
