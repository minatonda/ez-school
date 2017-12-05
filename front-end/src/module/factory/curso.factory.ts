import { NotifyUtil, MESSAGES, NOTIFY_TYPE } from '../util/notify.util';
import { BaseFactory } from './base.factory';
import { Curso } from '../model/server/curso';
import { CursoGrade } from '../model/server/curso-grade';

export class Factory extends BaseFactory {

    private  title = 'Curso';

    public async add(model: Curso, notify?: boolean) {
        try {
            let result = await this.put('/api/curso/add', model) as Curso;
            NotifyUtil.notify(MESSAGES.REGISTRO_ADD, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_ADD_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public async update(model: Curso, notify?: boolean) {
        try {
            let result = await this.post('/api/curso/update', model) as Curso;
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
            let result = await this.delete('/api/curso/disable', { params: { id: id } });
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
            let result = await this.get(`/api/curso/${id}`) as Curso;
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
            let result = await this.get('/api/curso') as Array<Curso>;
            NotifyUtil.notify(MESSAGES.REGISTRO_GET, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_GET_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public async allGrade(id: string, notify?: boolean) {
        try {
            let result = await this.get(`/api/curso/${id}/grade`) as Array<CursoGrade>;
            NotifyUtil.notify(MESSAGES.REGISTRO_GET, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_GET_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public async addGrade(id: string, model: CursoGrade, notify?: boolean) {
        try {
            let result = await this.put(`/api/curso/${id}/grade/add`, model) as CursoGrade;
            NotifyUtil.notify(MESSAGES.REGISTRO_ADD, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_ADD_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public async updateGrade(id: string, model: CursoGrade, notify?: boolean) {
        try {
            let result = await this.post(`/api/curso/${id}/grade/update`, model) as CursoGrade;
            NotifyUtil.notify(MESSAGES.REGISTRO_ADD, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_ADD_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

}

export const CursoFactory = new Factory();