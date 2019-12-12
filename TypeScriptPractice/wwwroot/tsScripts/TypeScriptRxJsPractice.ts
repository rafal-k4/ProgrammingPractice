
import { Subscription, fromEvent } from 'rxjs';

export class RxJsPractice {

    subscription: Subscription;

    constructor() {
        const source = fromEvent(document.getElementById('testInputId'), 'focusout');
        this.subscription = source.subscribe(val => console.log(val));
    }

    TestMethod(): void {
        console.log("Test from TestMethod");
    }
}