import { BaseError } from './base.error';

export class RedirectError extends BaseError {

    constructor(title: string, message: string, url: string) {
        super('Redirect Errror', title, message);
        this.url = url;
    }

    url: string;
}