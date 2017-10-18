import { Factory } from '../factory';
import { Autenticacao } from './autenticacao';
import { Usuario } from './usuario';
import { Notify, MESSAGES, NOTIFY_TYPE } from '../../notify/notify';
import { Aluno } from '../aluno/aluno';

export class UsuarioFactory extends Factory {

    private static title = 'Usuario';

    public static async autenticar(vm: Usuario, notify?: boolean) {
        try {
            let result = await this.post('/api/login', vm) as Autenticacao;
            Notify.notify(MESSAGES.LOGIN, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            Notify.notify(MESSAGES.LOGIN_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async add(model: Usuario, notify?: boolean) {
        try {
            let result = await this.put('/api/usuario/add', model) as Usuario;
            Notify.notify(MESSAGES.REGISTRO_ADD, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            Notify.notify(MESSAGES.REGISTRO_ADD_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async update(model: Usuario, notify?: boolean) {
        try {
            let result = await this.post('/api/usuario/update', model) as Usuario;
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
            let result = await this.delete('/api/usuario/disable', { params: { id: id } });
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
            let result = await this.get(`/api/usuario/${id}`) as Usuario;
            Notify.notify(MESSAGES.REGISTRO_GET, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            Notify.notify(MESSAGES.REGISTRO_GET_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async detailAluno(id: string, notify?: boolean) {
        try {
            let result = await this.get(`/api/usuario/${id}/aluno`) as Aluno;
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
            let result = await this.get('/api/usuario') as Array<Usuario>;
            Notify.notify(MESSAGES.REGISTRO_GET, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            Notify.notify(MESSAGES.REGISTRO_GET_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async updateAluno(id: string, model: Aluno, notify?: boolean) {
        try {
            let result = await this.post(`/api/usuario/${id}/aluno/update`, model) as Aluno;
            Notify.notify(MESSAGES.REGISTRO_UPD, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            Notify.notify(MESSAGES.REGISTRO_UPD_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

}