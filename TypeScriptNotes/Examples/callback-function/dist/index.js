"use strict";
function fn() {
    return fn2(() => {
        console.log("fn");
        return "abc";
    });
}
function fn2(callback) {
    console.log("fn2");
    return callback();
}
var result = fn();
console.log(result);
// async/await
async function fnAsync() {
    return await fnAsync2(async () => {
        console.log("fnAsync");
        return "abc";
    });
}
async function fnAsync2(callback) {
    console.log("fnAsync2");
    return await callback();
}
var result2 = fnAsync().then();
console.log(result2);
