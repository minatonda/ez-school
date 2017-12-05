import { NotifyUtil, MESSAGES, NOTIFY_TYPE } from '../util/notify.util';
import { BaseFactory } from './base.factory';
import { InstituicaoCategoria } from '../model/server/instituicao-categoria';

export class Factory extends BaseFactory {

    private title = 'Instituicao Categoria';

    public async add(model: InstituicaoCategoria, notify?: boolean) {
        try {
            let result = await this.put('/api/instituicao-categoria/add', model) as InstituicaoCategoria;
            NotifyUtil.notify(MESSAGES.REGISTRO_ADD, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_ADD_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public async update(model: InstituicaoCategoria, notify?: boolean) {
        try {
            let result = await this.post('/api/instituicao-categoria/update', model) as InstituicaoCategoria;
            NotifyUtil.notify(MESSAGES.REGISTRO_UPD, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_UPD_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public async disable(id: string, notify?: boolean) {
        try {
            let result = await this.delete('/api/instituicao-categoria/disable', { params: { id: id } });
            NotifyUtil.notify(MESSAGES.REGISTRO_DEL, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_DEL_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public async detail(id: string, notify?: boolean) {
        try {
            let result = await this.get(`/api/instituicao-categoria/${id}`) as InstituicaoCategoria;
            NotifyUtil.notify(MESSAGES.REGISTRO_GET, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_GET_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public async all(notify?: boolean) {
        try {
            let result = await this.get('/api/instituicao-categoria') as Array<InstituicaoCategoria>;
            NotifyUtil.notify(MESSAGES.REGISTRO_GET, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_GET_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

}

export const InstituicaoCategoriaFactory = new Factory();