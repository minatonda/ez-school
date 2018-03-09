import { BaseFactory } from './../base.factory';
import { UsuarioModel } from './../../model/server/usuario.model';
import { ProfessorModel } from './../../model/server/professor.model';
import { AlunoModel } from './../../model/server/aluno.model';
import { AutenticacaoModel } from './../../model/server/autenticacao.model';
import { UsuarioInfoModel } from '../../model/server/usuario-info.model';

export class Factory extends BaseFactory {

    private title = 'Usuario';

    public autenticar = async (vm: UsuarioModel) => {
        try {
            let result = await this.post('/api/login', vm) as AutenticacaoModel;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public add = async (model: UsuarioModel) => {
        try {
            let result = await this.put('/api/usuario/add', model) as UsuarioModel;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public update = async (model: UsuarioModel) => {
        try {
            let result = await this.post('/api/usuario/update', model) as UsuarioModel;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public disable = async (id: number | string) => {
        try {
            let result = await this.delete('/api/usuario/disable', { params: { id: id } });
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public detail = async (id: number | string) => {
        try {
            let result = await this.get(`/api/usuario/${id}`) as UsuarioModel;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public all = async () => {
        try {
            let result = await this.get('/api/usuario') as Array < UsuarioModel > ;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public me = async () => {
        try {
            let result = await this.get('/api/business/usuario/me') as UsuarioInfoModel;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

}