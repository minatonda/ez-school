import { Factory } from '../factory';
import { Instituicao } from './instituicao';
import { Notify, MESSAGES, NOTIFY_TYPE } from '../../notify/notify';
import { InstituicaoCurso } from './instituicao-curso';
import { InstituicaoCursoOcorrencia } from './instituicao-curso-ocorrencia';

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

    public static async update(model: Instituicao, notify?: boolean) {
        try {
            let result = await this.post('/api/instituicao/update', model) as Instituicao;
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
            let result = await this.delete('/api/instituicao/disable', { params: { id: id } });
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

    public static async allCurso(id: string, notify?: boolean) {
        try {
            let result = await this.get(`/api/instituicao/${id}/curso`) as Array<InstituicaoCurso>;
            Notify.notify(MESSAGES.REGISTRO_GET, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            Notify.notify(MESSAGES.REGISTRO_GET_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async detailCurso(id: string, idCurso: string, notify?: boolean) {
        try {
            let result = await this.get(`/api/instituicao/${id}/curso/${idCurso}`) as InstituicaoCurso;
            Notify.notify(MESSAGES.REGISTRO_GET, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            Notify.notify(MESSAGES.REGISTRO_GET_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async addCurso(id: string, model: InstituicaoCurso, notify?: boolean) {
        try {
            let result = await this.put(`/api/instituicao/${id}/curso/add`, model) as InstituicaoCurso;
            Notify.notify(MESSAGES.REGISTRO_ADD, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            Notify.notify(MESSAGES.REGISTRO_ADD_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async renewCurso(id: string, model: InstituicaoCurso, notify?: boolean) {
        try {
            let result = await this.post(`/api/instituicao/${id}/curso/renew`, model) as InstituicaoCurso;
            Notify.notify(MESSAGES.REGISTRO_ADD, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            Notify.notify(MESSAGES.REGISTRO_ADD_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async allCursoOcorrencia(id: string, idCurso: string, notify?: boolean) {
        try {
            let result = await this.get(`/api/instituicao/${id}/curso/${idCurso}/ocorrencia`) as Array<InstituicaoCursoOcorrencia>;
            Notify.notify(MESSAGES.REGISTRO_GET, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            Notify.notify(MESSAGES.REGISTRO_GET_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async detailCursoOcorrencia(id: string, idCurso: string, idOcorrencia: string, notify?: boolean) {
        try {
            let result = await this.get(`/api/instituicao/${id}/curso/${idCurso}/ocorrencia/${idOcorrencia}`) as InstituicaoCursoOcorrencia;
            Notify.notify(MESSAGES.REGISTRO_GET, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            Notify.notify(MESSAGES.REGISTRO_GET_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async addCursoOcorrencia(id: string, idCurso: string, model: InstituicaoCurso, notify?: boolean) {
        try {
            let result = await this.put(`/api/instituicao/${id}/curso/${idCurso}/ocorrencia/add`, model) as InstituicaoCursoOcorrencia;
            Notify.notify(MESSAGES.REGISTRO_ADD, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            Notify.notify(MESSAGES.REGISTRO_ADD_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async updateCursoOcorrencia(id: string, idCurso: string, model: InstituicaoCurso, notify?: boolean) {
        try {
            let result = await this.post(`/api/instituicao/${id}/curso/${idCurso}/ocorrencia/update`, model) as InstituicaoCursoOcorrencia;
            Notify.notify(MESSAGES.REGISTRO_ADD, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            Notify.notify(MESSAGES.REGISTRO_ADD_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async disableCursoOcorrencia(id: string, idCurso: string, model: InstituicaoCurso, notify?: boolean) {
        try {
            let result = await this.post(`/api/instituicao/${id}/curso/${idCurso}/ocorrencia/disable`, model) as InstituicaoCursoOcorrencia;
            Notify.notify(MESSAGES.REGISTRO_ADD, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            Notify.notify(MESSAGES.REGISTRO_ADD_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }


}