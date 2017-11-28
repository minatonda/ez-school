export class BaseError implements Error {

    constructor(name: string, message: string) {
        this.name = name;
        this.message = message;
    }

    name: string;
    message: string;
}

export class RedirectError extends BaseError {
    constructor(message: string, url: string) {
        super('Redirect Errror', message);
        this.url = url;
    }
    url: string;
}

export class RequestError extends BaseError {
    constructor(message: string, url: string, statusCode: number) {
        super('Request Error', message);
        this.url = url;
        this.statusCode = statusCode;
    }
    url: string;
    statusCode: number;
}