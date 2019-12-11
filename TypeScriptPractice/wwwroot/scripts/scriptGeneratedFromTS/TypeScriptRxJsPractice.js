"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var rxjs_1 = require("rxjs");
var RxJsPractice = /** @class */ (function () {
    function RxJsPractice() {
        var source = rxjs_1.fromEvent(document.getElementById('testInputId'), 'focusout');
        this.subscription = source.subscribe(function (val) { return console.log(val); });
    }
    return RxJsPractice;
}());
exports.RxJsPractice = RxJsPractice;
//# sourceMappingURL=TypeScriptRxJsPractice.js.map