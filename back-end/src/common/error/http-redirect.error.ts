import { BaseError } from "./base.error";

export class HttpRedirectError extends BaseError {

	constructor(message:string,url:string){
		super('HttpRedirectError',message);
		this.url = url;
	}
	url:string;

}