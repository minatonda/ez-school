import axios, { AxiosRequestConfig } from 'axios';
import { RequestError } from '../model/client/error';
import { Vue } from 'vue-property-decorator';
import _ from 'lodash';
import { AutenticacaoService } from '../service/autenticacao.service';
import { Logger } from '../log';
export class BaseFactory extends Vue {

    private baseConfig = {
        baseURL: process.env.API_URL,
        timeout: 60000,
        headers: {

        }
    };

    private logger = new Logger();

    constructor() {
        super();
    }

    private remakeRequest = async(config: AxiosRequestConfig) => {
        try {
            let newConfig = Object.assign({}, config);
            let result = await axios.request(newConfig);
            return result;
        }
        catch (error) {
            throw new RequestError(error, 'url', error.statusCode);
        }
    }

    protected post = async(url: string, body: any, config ?: any) => {
        try {
            let result = await axios.post(url, body, this.getConfig(config));
            return result.data;
        }
        catch (error) {
            this.logger.error(error);
            throw new RequestError(error, 'url', error.statusCode);
        }
    }

    protected put = async(url: string, body: any, config ?: any) => {
        try {
            let result = await axios.put(url, body, this.getConfig(config));
            return result.data;
        }
        catch (error) {
            this.logger.error(error);
            throw new RequestError(error, 'url', error.statusCode);
        }
    }

    protected get = async(url: string, config ?: any) => {
        try {
            let result = await axios.get(url, this.getConfig(config));
            return result.data;
        }
        catch (error) {
            this.logger.error(error);
            throw new RequestError(error, 'url', error.statusCode);
        }
    }

    protected delete = async(url: string, config ?: any) => {
        try {
            let result = await axios.delete(url, this.getConfig(config));
            return result.data;
        }
        catch (error) {
            this.logger.error(error);
            throw new RequestError(error, 'url', error.statusCode);
        }
    }

    private getConfig(config ?: AxiosRequestConfig) {
        let configInitial = _.merge({}, this.baseConfig);
        if (AutenticacaoService.isAutenticado()) {
            configInitial.headers['Authorization'] = ('bearer ' + AutenticacaoService.getToken());
        }
        return _.merge(configInitial, config) as AxiosRequestConfig;
    }

}