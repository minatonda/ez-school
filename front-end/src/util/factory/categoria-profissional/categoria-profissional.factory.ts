import { Factory } from '../factory';
import { CategoriaProfissional } from './categoria-profissional';
import { Notify, MESSAGES, NOTIFY_TYPE } from '../../notify/notify';

export class CategoriaProfissionalFactory extends Factory {

    private static title = 'CategoriaProfissional';

    public static async add(model: CategoriaProfissional, notify?: boolean) {
        try {
            let result = await this.put('/api/categoria-profissional/add', model) as CategoriaProfissional;
            Notify.notify(MESSAGES.REGISTRO_ADD, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            Notify.notify(MESSAGES.REGISTRO_ADD_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async update(model: CategoriaProfissional, notify?: boolean) {
        try {
            let result = await this.post('/api/categoria-profissional/update', model) as CategoriaProfissional;
            Notify.notify(MESSAGES.REGISTRO_UPD, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            Notify.notify(MESSAGES.REGISTRO_UPD_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async disable(id: string, notify?: boolean) {
        try {
            let result = await this.delete('/api/categoria-profissional/disable', { params: { id: id } });
            Notify.notify(MESSAGES.REGISTRO_DEL, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            Notify.notify(MESSAGES.REGISTRO_DEL_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async detail(id: string, notify?: boolean) {
        try {
            let result = await this.get(`/api/categoria-profissional/${id}`) as CategoriaProfissional;
            Notify.notify(MESSAGES.REGISTRO_GET, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            Notify.notify(MESSAGES.REGISTRO_GET_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async all(notify?: boolean) {
        try {
            let result = await this.get('/api/categoria-profissional') as Array<CategoriaProfissional>;
            Notify.notify(MESSAGES.REGISTRO_GET, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            Notify.notify(MESSAGES.REGISTRO_GET_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

}