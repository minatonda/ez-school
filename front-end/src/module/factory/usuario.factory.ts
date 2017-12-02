import { NotifyUtil, MESSAGES, NOTIFY_TYPE } from '../util/notify.util';
import { BaseFactory } from './base.factory';
import { Usuario } from '../model/server/usuario';
import { Autenticacao } from '../model/server/autenticacao';
import { Aluno } from '../model/server/aluno';
import { Professor } from '../model/server/professor';

export class Factory extends BaseFactory {

    private  title = 'Usuario';

    public async autenticar(vm: Usuario, notify?: boolean) {
        try {
            let result = await this.post('/api/login', vm) as Autenticacao;
            NotifyUtil.notify(MESSAGES.LOGIN, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.LOGIN_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public async add(model: Usuario, notify?: boolean) {
        try {
            let result = await this.put('/api/usuario/add', model) as Usuario;
            NotifyUtil.notify(MESSAGES.REGISTRO_ADD, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_ADD_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public async update(model: Usuario, notify?: boolean) {
        try {
            let result = await this.post('/api/usuario/update', model) as Usuario;
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
            let result = await this.delete('/api/usuario/disable', { params: { id: id } });
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
            let result = await this.get(`/api/usuario/${id}`) as Usuario;
            NotifyUtil.notify(MESSAGES.REGISTRO_GET, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_GET_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public async detailAluno(id: string, notify?: boolean) {
        try {
            let result = await this.get(`/api/usuario/${id}/aluno`) as Aluno;
            NotifyUtil.notify(MESSAGES.REGISTRO_GET, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_GET_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public async detailProfessor(id: string, notify?: boolean) {
        try {
            let result = await this.get(`/api/usuario/${id}/professor`) as Professor;
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
            let result = await this.get('/api/usuario') as Array<Usuario>;
            NotifyUtil.notify(MESSAGES.REGISTRO_GET, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_GET_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public async allAluno(termo: string, notify?: boolean) {
        try {
            let result = await this.get(`/api/usuario/aluno`, { params: { termo: termo } }) as Array<Aluno>;
            NotifyUtil.notify(MESSAGES.REGISTRO_GET, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_GET_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public async allProfessor(termo: string, notify?: boolean) {
        try {
            let result = await this.get(`/api/usuario/professor`, { params: { termo: termo } }) as Array<Aluno>;
            NotifyUtil.notify(MESSAGES.REGISTRO_GET, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_GET_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public async updateAluno(id: string, model: Aluno, notify?: boolean) {
        try {
            let result = await this.post(`/api/usuario/${id}/aluno/update`, model) as Aluno;
            NotifyUtil.notify(MESSAGES.REGISTRO_UPD, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_UPD_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public async updateProfessor(id: string, model: Professor, notify?: boolean) {
        try {
            let result = await this.post(`/api/usuario/${id}/professor/update`, model) as Professor;
            NotifyUtil.notify(MESSAGES.REGISTRO_UPD, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_UPD_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

}

export const UsuarioFactory = new Factory();