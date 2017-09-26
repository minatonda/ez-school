import { Factory } from '../factory';
import { Curso } from './curso';
import { Notify, NOTIFY_TYPE, MESSAGES } from '../../modules/notify/notify';

export class CursoFactory extends Factory {

    private static title = 'Curso';

    public static async add(model: Curso, notify?: boolean) {
        try {
            let result = await this.put('/api/curso/add', model) as Curso;
            Notify.notify(MESSAGES.REGISTRO_ADD, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            Notify.notify(MESSAGES.REGISTRO_ADD_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async upd(model: Curso, notify?: boolean) {
        try {
            let result = await this.post('/api/curso/upd', model) as Curso;
            Notify.notify(MESSAGES.REGISTRO_UPD, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            Notify.notify(MESSAGES.REGISTRO_UPD_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async del(id: string, notify?: boolean) {
        try {
            let result = await this.delete('/api/curso/del/' + id);
            Notify.notify(MESSAGES.REGISTRO_DEL, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            Notify.notify(MESSAGES.REGISTRO_DEL_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async dtl(id: string, notify?: boolean) {
        try {
            let result = await this.get('/api/curso/dtl/' + id) as Curso;
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
            let result = await this.get('/api/curso') as Array<Curso>;
            Notify.notify(MESSAGES.REGISTRO_GET, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            Notify.notify(MESSAGES.REGISTRO_GET_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

}