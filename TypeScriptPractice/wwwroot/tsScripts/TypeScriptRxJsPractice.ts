
import { Subscription, fromEvent } from 'rxjs';
import * as $ from 'jquery'

export function TestExportFunction() {
    let entryPoint = new RxJsPractice();

    entryPoint.ApiCall();
}

export class RxJsPractice {
    
    subscription: Subscription;

    constructor() {
        let btn = document.getElementById("testButtonId");
        btn.addEventListener('click', (e: Event) => { console.log("test"); alert("AAAA")})
        const source = fromEvent(document.getElementById('testInputId'), 'focusout');
        this.subscription = source.subscribe(val => console.log(val));
    }



    async ApiCall() {
        console.log('----------BEFORE1---------');
        let something = await $.get('https://my-json-server.typicode.com/rafal-k4/FakeApi/pilots');
        console.log('----------AFTER1---------', something);

        console.log("BEFORE PROMISE")
        var promise = await new Promise(resolve => {
            setTimeout(null, 2000);
            $.get('https://my-json-server.typicode.com/rafal-k4/FakeApi/pilots')
                .done(function (response) {
                    resolve(response);
                });
        });
        
        console.log("AFTER PROMISE", promise);

    }
}
