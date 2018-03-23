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

    public disable = async (id: number | string) => {
        try {
            let result = await this.delete('/api/curso/disable', { params: { id: id } });
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public detail = async (id: number | string) => {
        try {
            let result = await this.get(`/api/curso/detail/${id}`) as CursoModel;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public all = async () => {
        try {
            let result = await this.get('/api/curso') as Array<CursoModel>;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public allCursoGrade = async (id: number | string) => {
        try {
            let result = await this.get(`/api/curso/detail/${id}/curso-grade`) as Array<CursoGradeModel>;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public allCursoGradeByInstituicao = async (id: number | string, idInstituicao: number | string) => {
        try {
            let result = await this.get(`/api/curso/detail/${id}/curso-grade/by-instituicao/${idInstituicao}`) as Array<CursoGradeModel>;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public allCursoGradeMateria = async (id: number | string, idCursoGrade: number | string) => {
        try {
            let result = await this.get(`/api/curso/detail/${id}/curso-grade/detail/${idCursoGrade}/curso-grade-materia`) as Array<CursoGradeModel>;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

}