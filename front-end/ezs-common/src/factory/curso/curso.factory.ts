import { BaseFactory } from './../base.factory';
import { CursoModel } from './../../model/server/curso.model';
import { CursoGradeModel } from './../../model/server/curso-grade.model';

export class Factory extends BaseFactory {

    private title = 'Curso';

    public add = async (model: CursoModel) => {
        try {
            let result = await this.put('/api/curso/add', model) as CursoModel;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public update = async (model: CursoModel) => {
        try {
            let result = await this.post('/api/curso/update', model) as CursoModel;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public disable = async (id: string) => {
        try {
            let result = await this.delete('/api/curso/disable', { params: { id: id } });
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public detail = async (id: string) => {
        try {
            let result = await this.get(`/api/curso/${id}`) as CursoModel;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public all = async () => {
        try {
            let result = await this.get('/api/curso') as Array < CursoModel > ;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public allCursoGrade = async (id: string) => {
        try {
            let result = await this.get(`/api/curso/${id}/curso-grade`) as Array < CursoGradeModel > ;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public allCursoGradeMateria = async (id: string, idCursoGrade: string) => {
        try {
            let result = await this.get(`/api/curso/${id}/curso-grade/${idCursoGrade}/curso-grade-materia`) as Array < CursoGradeModel > ;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

}