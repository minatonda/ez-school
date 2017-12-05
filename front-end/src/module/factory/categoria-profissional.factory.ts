import { NotifyUtil, MESSAGES, NOTIFY_TYPE } from '../util/notify.util';
import { BaseFactory } from './base.factory';
import { CategoriaProfissional } from '../model/server/categoria-profissional';

export class Factory extends BaseFactory {

    private title = 'CategoriaProfissional';

    public async add(model: CategoriaProfissional, notify ?: boolean) {
        try {
            let result = await this.put('/api/categoria-profissional/add', model) as CategoriaProfissional;
            NotifyUtil.notify(MESSAGES.REGISTRO_ADD, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_ADD_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public async update(model: CategoriaProfissional, notify ?: boolean) {
        try {
            let result = await this.post('/api/categoria-profissional/update', model) as CategoriaProfissional;
            NotifyUtil.notify(MESSAGES.REGISTRO_UPD, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_UPD_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public async disable(id: string, notify ?: boolean) {
        try {
            let result = await this.delete('/api/categoria-profissional/disable', { params: { id: id } });
            NotifyUtil.notify(MESSAGES.REGISTRO_DEL, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_DEL_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public async detail(id: string, notify ?: boolean) {
        try {
            let result = await this.get(`/api/categoria-profissional/${id}`) as CategoriaProfissional;
            NotifyUtil.notify(MESSAGES.REGISTRO_GET, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_GET_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public async all(notify ?: boolean) {
        try {
            let result = await this.get('/api/categoria-profissional') as Array < CategoriaProfissional > ;
            NotifyUtil.notify(MESSAGES.REGISTRO_GET, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_GET_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

}

export const CategoriaProfissionalFactory = new Factory();