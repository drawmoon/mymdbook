function fn(): string {
  return fn2((): string => {
    console.log("fn");
    return "abc";
  });
}

function fn2(callback: () => string): string {
  console.log("fn2");
  return callback();
}

var result = fn();
console.log(result);

// async/await

async function fnAsync(): Promise<string> {
  return await fnAsync2(
    async (): Promise<string> => {
      console.log("fnAsync");
      return "abc";
    }
  );
}

async function fnAsync2(callback: () => Promise<string>): Promise<string> {
  console.log("fnAsync2");
  return await callback();
}

var result2 = fnAsync();
console.log(result2);
