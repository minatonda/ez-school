import { Factory } from '../factory';
import { Instituicao } from './instituicao';
import { Notify, MESSAGES, NOTIFY_TYPE } from '../../notify/notify';
import { InstituicaoCurso } from './instituicao-curso';

export class InstituicaoFactory extends Factory {

    private static title = 'Instituicao';

    public static async add(model: Instituicao, notify?: boolean) {
        try {
            let result = await this.put('/api/instituicao/add', model) as Instituicao;
            Notify.notify(MESSAGES.REGISTRO_ADD, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            Notify.notify(MESSAGES.REGISTRO_ADD_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async upd(model: Instituicao, notify?: boolean) {
        try {
            let result = await this.post('/api/instituicao/upd', model) as Instituicao;
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
            let result = await this.delete('/api/instituicao/del', { params: { id: id } });
            Notify.notify(MESSAGES.REGISTRO_DEL, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            Notify.notify(MESSAGES.REGISTRO_DEL_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async dtl(id: number, notify?: boolean) {
        try {
            let result = await this.get(`/api/instituicao/${id}`) as Instituicao;
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
            let result = await this.get('/api/instituicao') as Array<Instituicao>;
            Notify.notify(MESSAGES.REGISTRO_GET, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            Notify.notify(MESSAGES.REGISTRO_GET_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async getCursos(id: number, notify?: boolean) {
        try {
            let result = await this.get(`/api/instituicao/${id}/cursos`) as Array<InstituicaoCurso>;
            Notify.notify(MESSAGES.REGISTRO_GET, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            Notify.notify(MESSAGES.REGISTRO_GET_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async addCurso(id: number, model: InstituicaoCurso, notify?: boolean) {
        try {
            let result = await this.put(`/api/instituicao/${id}/cursos/add`, model) as InstituicaoCurso;
            Notify.notify(MESSAGES.REGISTRO_ADD, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            Notify.notify(MESSAGES.REGISTRO_ADD_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

}