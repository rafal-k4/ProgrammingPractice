
class TypeScriptPracticeClass {

    someProp: string;

    constructor() {
        this.someProp = "asd";
    }

    async MethodAsync(): Promise<string> {
        return await this.DoSomething(this.someProp);
    }

    async DoSomething(input: string): Promise<string> {
        return input;
    }
}

