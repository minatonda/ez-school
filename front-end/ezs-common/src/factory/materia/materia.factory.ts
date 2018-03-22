import { BaseFactory } from './../base.factory';
import { MateriaModel } from './../../model/server/materia.model';

export class Factory extends BaseFactory {

    private title = 'Materia';

    public add = async (model: MateriaModel) => {
        try {
            let result = await this.put('/api/materia/add', model) as MateriaModel;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public update = async (model: MateriaModel) => {
        try {
            let result = await this.post('/api/materia/update', model) as MateriaModel;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public disable = async (id: number | string) => {
        try {
            let result = await this.delete('/api/materia/disable', { params: { id: id } });
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public detail = async (id: number | string) => {
        try {
            let result = await this.get(`/api/materia/detail/${id}`) as MateriaModel;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public all = async () => {
        try {
            let result = await this.get('/api/materia') as Array < MateriaModel > ;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

}