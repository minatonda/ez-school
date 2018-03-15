export class BaseError implements Error {

    constructor(name: string, title: string, message: string) {
        this.name = name;
        this.title = title;
        this.message = message;
    }

    name: string;
    title: string;
    message: string;
}