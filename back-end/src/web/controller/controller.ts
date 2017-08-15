import { Request, Response } from "express";
import { BaseError } from "../../common/error/base.error";
import { HttpRedirectError } from "../../common/error/http-redirect.error";
import { HttpRequestError } from "../../common/error/http-request.error";

export class Controller {

    constructor(title: string) {
        this._title = title;
    }

    protected _title: string;

    protected _resolveError(req: Request, res: Response, error: Error) {
        //logger.error(this._title);
        //logger.error((error as BaseError).name);
        //logger.error((error as BaseError).message);
        switch ((error.constructor)) {
        case (BaseError):
            {
                this._resolveBaseError(req, res, (error as BaseError));
                break;
            };
        case (HttpRedirectError):
            {
                this._resolveHttpRedirectError(req, res, (error as HttpRedirectError));
                break;
            };
        case (HttpRequestError):
            {
                this._resolveHttpRequestError(req, res, (error as HttpRequestError));
                break;
            };
        default:
            {
                this._resolveDefaultError(req, res, error);
                break;
            }
        }
    }

    private _resolveDefaultError(req: Request, res: Response, error: Error) {
        res.redirect("/");
    }

    private _resolveBaseError(req: Request, res: Response, error: BaseError) {
        res.redirect("/");
    }

    private _resolveHttpRedirectError(req: Request, res: Response, error: HttpRedirectError) {
        //logger.error(error.url);
        res.redirect(error.url);
    }

    private _resolveHttpRequestError(req: Request, res: Response, error: HttpRequestError) {
        //logger.error(error.url);
        switch (error.statusCode) {
        case (404):
            {
                //res.status(error.statusCode).send('O recurso solicitado não foi encontrado.');
                res.redirect("not-found");
                break;
            };
        default:
            {
                res.redirect("/");
                break;
            };
        }
    }

}