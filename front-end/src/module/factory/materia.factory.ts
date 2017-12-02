import { NotifyUtil, MESSAGES, NOTIFY_TYPE } from '../util/notify.util';
import { BaseFactory } from './base.factory';
import { Materia } from '../model/server/materia';

export class Factory extends BaseFactory {

    private title = 'Materia';

    public async add(model: Materia, notify?: boolean) {
        try {
            let result = await this.put('/api/materia/add', model) as Materia;
            NotifyUtil.notify(MESSAGES.REGISTRO_ADD, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_ADD_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public async update(model: Materia, notify?: boolean) {
        try {
            let result = await this.post('/api/materia/update', model) as Materia;
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
            let result = await this.delete('/api/materia/disable', { params: { id: id } });
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
            let result = await this.get(`/api/materia/${id}`) as Materia;
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
            let result = await this.get('/api/materia') as Array<Materia>;
            NotifyUtil.notify(MESSAGES.REGISTRO_GET, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_GET_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

}

export const MateriaFactory = new Factory();