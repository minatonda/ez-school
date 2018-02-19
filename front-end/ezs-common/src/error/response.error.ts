import { BaseError } from './base.error';
import { ErrorResponseType } from './response-type.error';

export class ErrorResponse extends BaseError {
    
    constructor(message: string, url: string, statusCode: number) {
        super('Request Error', message);
        this.url = url;
        this.statusCode = statusCode;
    }

    url: string;
    statusCode: number;
    code: string;
    text: string;
    timeStamp: string;
    properties: Array<ErrorResponseType>;
    children: Array<ErrorResponse>;
}