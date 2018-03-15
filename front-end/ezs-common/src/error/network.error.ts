import { BaseError } from './base.error';

export class NetworkError extends BaseError {

    constructor(title: string, message: string, url: string) {
        super('Network Errror', title, message);
        this.url = url;
    }
    
    url: string;
}