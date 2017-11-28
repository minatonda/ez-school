import axios from 'axios';
import _ from 'lodash';
import { RequestError } from '../model/client/error';
import { AutenticacaoService } from '../service/autenticacao.service';
export class Factory {

    private static apiUrl = 'http://localhost:5000';
    protected static joinUrl(url: string) {
        return this.apiUrl + url;
    }

    protected static async post(url: string, body: any, config?: any) {
        try {
            let result = await axios.post(this.joinUrl(url), body, this.joinConfig(config));
            return result.data;
        } catch (error) {
            throw new RequestError(error.error, 'url', error.statusCode);
        }
    }

    protected static async put(url: string, body: any, config?: any) {
        try {
            let result = await axios.put(this.joinUrl(url), body, this.joinConfig(config));
            return result.data;
        } catch (error) {
            throw new RequestError(error.error, 'url', error.statusCode);
        }
    }

    protected static async get(url: string, config?: any) {
        try {
            let result = await axios.get(this.joinUrl(url), this.joinConfig(config));
            return result.data;
        } catch (error) {
            throw new RequestError(error.error, 'url', error.statusCode);
        }
    }

    protected static async delete(url: string, config?: any) {
        try {
            let result = await axios.delete(this.joinUrl(url), this.joinConfig(config));
            return result.data;
        } catch (error) {
            throw new RequestError(error.error, 'url', error.statusCode);
        }
    }

    protected static joinConfig(config) {
        let copy_cfg = JSON.parse(JSON.stringify(config || {}));
        if (!copy_cfg.headers) {
            copy_cfg.headers = {};
        }
        if (AutenticacaoService.isAutenticado()) {
            copy_cfg.headers['Authorization'] = 'bearer ' + AutenticacaoService.getToken();
        }
        return copy_cfg;
    }


}