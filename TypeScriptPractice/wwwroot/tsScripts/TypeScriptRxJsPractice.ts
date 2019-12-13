
import { Subscription, fromEvent } from 'rxjs';

export function TestExportFunction() {
    let entryPoint = new RxJsPractice();
    entryPoint.TestMethod();

    entryPoint.ApiCall();
}

export class RxJsPractice {
    
    subscription: Subscription;

    constructor() {
        let btn = document.getElementById("testButtonId");
        btn.addEventListener('click', (e: Event) => { this.TestMethod(); console.log("test"); alert("AAAA")})
        const source = fromEvent(document.getElementById('testInputId'), 'focusout');
        this.subscription = source.subscribe(val => console.log(val));
    }

    TestMethod(): void {
        console.log("Test from TestMethod");
    }

    ApiCall() {
        console.log("hello from api call method");
    }
}
