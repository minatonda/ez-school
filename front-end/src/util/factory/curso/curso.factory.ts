import { Factory } from '../factory';
import { Curso } from './curso';
import { CursoGrade } from './curso-grade';
import { Materia } from '../materia/materia';
import { Notify, MESSAGES, NOTIFY_TYPE } from '../../notify/notify';

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
            let result = await this.delete('/api/curso/del', { params: { id: id } });
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
            let result = await this.get(`/api/curso/${id}`) as Curso;
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

    public static async getGrades(id: number, notify?: boolean) {
        try {
            let result = await this.get(`/api/curso/${id}/grades`) as Array<CursoGrade>;
            Notify.notify(MESSAGES.REGISTRO_GET, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            Notify.notify(MESSAGES.REGISTRO_GET_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async addGrade(id: number, model: CursoGrade, notify?: boolean) {
        try {
            let result = await this.put(`/api/curso/${id}/grades/add`, model) as CursoGrade;
            Notify.notify(MESSAGES.REGISTRO_ADD, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            Notify.notify(MESSAGES.REGISTRO_ADD_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async addGradeMaterias(id: number, idGrade: number, model: Materia, notify?: boolean) {
        try {
            let result = await this.put(`/api/curso/${id}/grades/${idGrade}/materias/add`, model) as Materia;
            Notify.notify(MESSAGES.REGISTRO_ADD, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            Notify.notify(MESSAGES.REGISTRO_ADD_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async removeGradeMaterias(id: number, idGrade: number, idMateria: number, notify?: boolean) {
        try {
            let result = await this.delete(`/api/curso/${id}/grades/${idGrade}/materias/del`, { params: { idMateria: id } });
            Notify.notify(MESSAGES.REGISTRO_DEL, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            Notify.notify(MESSAGES.REGISTRO_DEL_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

}