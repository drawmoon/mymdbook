"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
function ExceptionFilter() {
    return function (target, propertyKey, descriptor) {
        var original = descriptor.value;
        descriptor.value = function () {
            try {
                original.apply(this);
            }
            catch (e) {
                console.log(e);
            }
        };
    };
}
let C = /** @class */ (() => {
    class C {
        fn() {
            console.log("test");
            throw new Error("error");
        }
    }
    __decorate([
        ExceptionFilter()
    ], C.prototype, "fn", null);
    return C;
})();
var c = new C();
c.fn();
