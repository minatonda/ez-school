import { BaseFactory } from './../base.factory';
import { CategoriaProfissionalModel } from './../../model/server/categoria-profissional.model';

export class Factory extends BaseFactory {

    private title = 'CategoriaProfissional';

    public add = async (model: CategoriaProfissionalModel) => {
        try {
            let result = await this.put('/api/categoria-profissional/add', model) as CategoriaProfissionalModel;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public update = async (model: CategoriaProfissionalModel) => {
        try {
            let result = await this.post('/api/categoria-profissional/update', model) as CategoriaProfissionalModel;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public disable = async (id: number | string) => {
        try {
            let result = await this.delete('/api/categoria-profissional/disable', { params: { id: id } });
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public detail = async (id:  number | string) => {
        try {
            let result = await this.get(`/api/categoria-profissional/detail/${id}`) as CategoriaProfissionalModel;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public all = async () => {
        try {
            let result = await this.get('/api/categoria-profissional') as Array < CategoriaProfissionalModel > ;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

}