function doSome(): string {
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

console.log(doSome());

// 异步回调函数
async function doSomeAsync(callback: (id: string) => Promise<string>) {
  const tasks = ['1'];
  for (const task of tasks) {
    const str = await callback(task);
    console.log(str);
  }
}

doSomeAsync(async (id) => {
  // do something...
  return `Call task, id: <<${id}>>`;
});

// 泛型回调函数
function doSomeGeneric(callback: <T>(id: T) => string) {
  const tasks = ['1'];
  for (const task of tasks) {
    const str = callback(task);
    console.log(str);
  }
}

doSomeGeneric((id) => {
  // do something...
  return `Call task, id: <<${id}>>`;
});
