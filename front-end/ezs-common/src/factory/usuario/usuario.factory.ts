import { BaseFactory } from './../base.factory';
import { UsuarioModel } from './../../model/server/usuario.model';
import { ProfessorModel } from './../../model/server/professor.model';
import { AlunoModel } from './../../model/server/aluno.model';
import { AutenticacaoModel } from './../../model/server/autenticacao.model';

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

    public disable = async (id: string) => {
        try {
            let result = await this.delete('/api/usuario/disable', { params: { id: id } });
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public detail = async (id: string) => {
        try {
            let result = await this.get(`/api/usuario/${id}`) as UsuarioModel;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public detailAluno = async (id: string) => {
        try {
            let result = await this.get(`/api/usuario/${id}/aluno`) as AlunoModel;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public detailProfessor = async (id: string) => {
        try {
            let result = await this.get(`/api/usuario/${id}/professor`) as ProfessorModel;
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

    public allAluno = async (termo: string) => {
        try {
            let result = await this.get(`/api/usuario/aluno`, { params: { termo: termo } }) as Array < AlunoModel > ;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public allProfessor = async (termo: string) => {
        try {
            let result = await this.get(`/api/usuario/professor`, { params: { termo: termo } }) as Array < AlunoModel > ;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public updateAluno = async (id: string, model: AlunoModel) => {
        try {
            let result = await this.post(`/api/usuario/${id}/aluno/update`, model) as AlunoModel;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public updateProfessor = async (id: string, model: ProfessorModel) => {
        try {
            let result = await this.post(`/api/usuario/${id}/professor/update`, model) as ProfessorModel;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

}