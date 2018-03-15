import { BaseError } from './base.error';

export class RequestError extends BaseError {

    constructor(title: string, message: string, url: string, statusCode: number) {
        super('Request Errror', title, message);
        this.url = url;
        this.statusCode = statusCode;
    }

    url: string;
    statusCode: number;
}