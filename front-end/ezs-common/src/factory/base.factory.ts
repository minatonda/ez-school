import { AutenticaoServiceInterface } from '../service/autenticacao.service.interface';
import { NetworkError } from '../error/network.error';
import { RequestError } from '../error/request.error';
import axios, { AxiosInstance, AxiosRequestConfig } from 'axios';
import _ from 'lodash';


export class BaseFactory {

    private axios = axios.create();
    private interceptorOnRequestSuccess;
    private interceptorOnRequestError;
    protected authenticationService: AutenticaoServiceInterface;

    protected requestConfig: AxiosRequestConfig;

    constructor(authenticationService: AutenticaoServiceInterface, interceptorOnRequestSuccess: any, interceptorOnRequestError: any) {
        this.authenticationService = authenticationService;
        this.requestConfig = {};
        this.requestConfig.baseURL = process.env.API_URL;
        this.requestConfig.timeout = 60000;
        this.requestConfig.headers = {};
        this.interceptorOnRequestSuccess = interceptorOnRequestSuccess;
        // this.interceptorOnRequestError = interceptorOnRequestError;
        // this.axios.interceptors.response.use(this.onRequestSuccess, this.onRequestError);
    }

    protected post = async (url: string, body: any, config?: any) => {
        try {
            let result = await this.axios.post(url, body, this.getRequestConfigAssigned(config));
            return result.data;
        }
        catch (error) {
            throw error;
        }
    }

    protected put = async (url: string, body: any, config?: any) => {
        try {
            let result = await this.axios.put(url, body, this.getRequestConfigAssigned(config));
            return result.data;
        }
        catch (error) {
            throw error;
        }
    }

    protected get = async (url: string, config?: any) => {
        try {
            let result = await this.axios.get(url, this.getRequestConfigAssigned(config));
            return result.data;
        }
        catch (error) {
            throw error;
        }
    }

    protected delete = async (url: string, config?: any) => {
        try {
            let result = await this.axios.delete(url, this.getRequestConfigAssigned(config));
            return result.data;
        }
        catch (error) {
            throw error;
        }
    }

    private onRequestSuccess = (response) => {
        if (this.interceptorOnRequestSuccess) {
            return this.interceptorOnRequestSuccess(response);
        }
        else {
            return response;
        }
    }

    private onRequestError = (response) => {
        if (this.interceptorOnRequestSuccess) {
            return this.interceptorOnRequestError(response);
        }
        else {
            return Promise.reject(this.toError(response));
        }
    }

    private remakeRequest = async (config: AxiosRequestConfig) => {
        try {
            let newConfig = Object.assign({}, config);
            let result = await axios.request(newConfig);
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    private toError(error: any) {
        if (error.message === 'Network Error') {
            return new NetworkError('', error, error.config.url);
        }
        else {
            return new RequestError(error, error.config.url, error.statusCode, error.response.data);
        }
    }

    private getRequestConfigAssigned(config?: AxiosRequestConfig) {
        let configInitial = _.merge({}, this.requestConfig);
        if (this.authenticationService.isAutenticado()) {
            configInitial.headers['Authorization'] = ('bearer ' + this.authenticationService.getToken());
        }
        return _.merge(configInitial, config) as AxiosRequestConfig;
    }

}