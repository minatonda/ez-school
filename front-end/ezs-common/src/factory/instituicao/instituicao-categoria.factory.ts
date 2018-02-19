import { BaseFactory } from './../base.factory';
import { InstituicaoCategoriaModel } from './../../model/server/instituicao-categoria.model';

export class Factory extends BaseFactory {

    private title = 'Instituicao Categoria';

    public add = async (model: InstituicaoCategoriaModel) => {
        try {
            let result = await this.put('/api/instituicao-categoria/add', model) as InstituicaoCategoriaModel;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public update = async (model: InstituicaoCategoriaModel) => {
        try {
            let result = await this.post('/api/instituicao-categoria/update', model) as InstituicaoCategoriaModel;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public disable = async (id: string) => {
        try {
            let result = await this.delete('/api/instituicao-categoria/disable', { params: { id: id } });
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public detail = async (id: string) => {
        try {
            let result = await this.get(`/api/instituicao-categoria/${id}`) as InstituicaoCategoriaModel;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public all = async () => {
        try {
            let result = await this.get('/api/instituicao-categoria') as Array < InstituicaoCategoriaModel > ;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

}