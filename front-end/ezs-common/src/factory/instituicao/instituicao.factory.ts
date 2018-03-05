import { BaseFactory } from './../base.factory';
import { InstituicaoModel } from './../../model/server/instituicao.model';
import { InstituicaoCursoModel } from './../../model/server/instituicao-curso.model';
import { InstituicaoCursoOcorrenciaModel } from './../../model/server/instituicao-curso-ocorrencia.model';
import { InstituicaoCursoPeriodoModel } from './../../model/server/instituicao-curso-periodo.model';
import { CursoGradeMateriaModel } from './../../model/server/curso-grade-materia.model';
import { InstituicaoCursoTurmaModel } from './../../model/server/instituicao-curso-turma.model';

export class Factory extends BaseFactory {

    private title = 'Instituicao';

    public add = async (model: InstituicaoModel) => {
        try {
            let result = await this.put('/api/instituicao/add', model) as InstituicaoModel;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public addInstituicaoCurso = async (id: number | string, model: InstituicaoCursoModel) => {
        try {
            let result = await this.put(`/api/instituicao/${id}/instituicao-curso/add`, model) as InstituicaoCursoModel;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public addInstituicaoCursoOcorrencia = async (id: number | string, idInstituicaoCurso: number | string, model: InstituicaoCursoOcorrenciaModel) => {
        try {
            let result = await this.put(`/api/instituicao/${id}/instituicao-curso/${idInstituicaoCurso}/instituicao-curso-ocorrencia/add`, model) as InstituicaoCursoOcorrenciaModel;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public update = async (model: InstituicaoModel) => {
        try {
            let result = await this.post('/api/instituicao/update', model) as InstituicaoModel;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public updateInstituicaoCurso = async (id: number | string, model: InstituicaoCursoModel) => {
        try {
            let result = await this.post(`/api/instituicao/${id}/instituicao-curso/update`, model) as InstituicaoCursoModel;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public updateInstituicaoCursoOcorrencia = async (id: number | string, idInstituicaoCurso: number | string, model: InstituicaoCursoOcorrenciaModel) => {
        try {
            let result = await this.post(`/api/instituicao/${id}/instituicao-curso/${idInstituicaoCurso}/instituicao-curso-ocorrencia/update`, model) as InstituicaoCursoModel;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public detail = async (id: number | string) => {
        try {
            let result = await this.get(`/api/instituicao/${id}`) as InstituicaoModel;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public detailInstituicaoCurso = async (id: number | string, idInstituicaoCurso: number | string) => {
        try {
            let result = await this.get(`/api/instituicao/${id}/instituicao-curso/${idInstituicaoCurso}`) as InstituicaoCursoModel;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public detailInstituicaoCursoOcorrencia = async (id: number | string, idInstituicaoCurso: number | string, idInstituicaoCursoOcorrencia: number | string) => {
        try {
            let result = await this.get(`/api/instituicao/${id}/instituicao-curso/${idInstituicaoCurso}/instituicao-curso-ocorrencia/${idInstituicaoCursoOcorrencia}`) as InstituicaoCursoOcorrenciaModel;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public all = async () => {
        try {
            let result = await this.get('/api/instituicao') as Array < InstituicaoModel > ;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public allInstituicaoCurso = async (id: number | string) => {
        try {
            let result = await this.get(`/api/instituicao/${id}/instituicao-curso`) as Array < InstituicaoCursoModel > ;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public allInstituicaoCursoPeriodo = async (id: number | string, idInstituicaoCurso: number | string) => {
        try {
            let result = await this.get(`/api/instituicao/${id}/instituicao-curso/${idInstituicaoCurso}/instituicao-curso-periodo`) as Array < InstituicaoCursoPeriodoModel > ;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public allInstituicaoCursoTurma = async (id: number | string, idInstituicaoCurso: number | string) => {
        try {
            let result = await this.get(`/api/instituicao/${id}/instituicao-curso/${idInstituicaoCurso}/instituicao-curso-turma`) as Array < InstituicaoCursoTurmaModel > ;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public allCursoGradeMaterias = async (id: number | string, idInstituicaoCurso: number | string) => {
        try {
            let result = await this.get(`/api/instituicao/${id}/instituicao-curso/${idInstituicaoCurso}/curso-grade-materia`) as Array < CursoGradeMateriaModel > ;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public allInstituicaoCursoOcorrencia = async (id: number | string, idInstituicaoCurso: number | string) => {
        try {
            let result = await this.get(`/api/instituicao/${id}/instituicao-curso/${idInstituicaoCurso}/instituicao-curso-ocorrencia`) as Array < InstituicaoCursoOcorrenciaModel > ;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public disable = async (id: number | string) => {
        try {
            let result = await this.delete('/api/instituicao/disable', { params: { id: id } });
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public disableInstituicaoCurso = async (id: number | string, idInstituicaoCurso: number | string) => {
        try {
            let result = await this.delete(`/api/instituicao/${id}/instituicao-curso/disable`, { params: { idInstituicaoCurso: idInstituicaoCurso } });
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public disableInstituicaoCursoOcorrencia = async (id: number | string, idInstituicaoCurso: number | string, idInstituicaoCursoOcorrencia: number | string) => {
        try {
            let result = await this.delete(`/api/instituicao/${id}/instituicao-curso/${idInstituicaoCurso}/instituicao-curso-ocorrencia/disable`, { params: { idInstituicaoCursoOcorrencia: idInstituicaoCursoOcorrencia } });
            return result;
        }
        catch (error) {
            throw error;
        }
    }

}