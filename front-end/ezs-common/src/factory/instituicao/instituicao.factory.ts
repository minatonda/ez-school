import { BaseFactory } from './../base.factory';
import { InstituicaoModel } from './../../model/server/instituicao.model';
import { InstituicaoCursoModel } from './../../model/server/instituicao-curso.model';
import { InstituicaoCursoOcorrenciaModel } from './../../model/server/instituicao-curso-ocorrencia.model';
import { InstituicaoCursoPeriodoModel } from './../../model/server/instituicao-curso-periodo.model';
import { CursoGradeMateriaModel } from './../../model/server/curso-grade-materia.model';
import { InstituicaoCursoTurmaModel } from './../../model/server/instituicao-curso-turma.model';
import { InstituicaoCursoOcorrenciaPeriodoModel } from '../../model/server/instituicao-curso-ocorrencia-periodo.model';
import { InstituicaoBusinessAulaModel } from '../../model/server/instituicao-business-aula.model';
import { InstituicaoBusinessAulaDetalheAlunoModel } from '../../model/server/instituicao-business-aula-detalhe-aluno.model';
import { InstituicaoCursoOcorrenciaPeriodoAlunoModel } from '../../model/server/instituicao-curso-ocorrencia-periodo-aluno.model';
import { InstituicaoCursoOcorrenciaNotaModel } from '../../model/server/instituicao-curso-ocorrencia-nota.model';

export class Factory extends BaseFactory {

    private title = 'Instituicao';

    public add = async (model: InstituicaoModel) => {
        try {
            await this.put('/api/instituicao/add', model);
        }
        catch (error) {
            throw error;
        }
    }

    public addInstituicaoCurso = async (id: number | string, model: InstituicaoCursoModel) => {
        try {
            await this.put(`/api/instituicao/${id}/instituicao-curso/add`, model);
        }
        catch (error) {
            throw error;
        }
    }

    public addInstituicaoCursoOcorrencia = async (id: number | string, idInstituicaoCurso: number | string, model: InstituicaoCursoOcorrenciaModel) => {
        try {
            await this.put(`/api/instituicao/${id}/instituicao-curso/${idInstituicaoCurso}/instituicao-curso-ocorrencia/add`, model);
        }
        catch (error) {
            throw error;
        }
    }

    public update = async (model: InstituicaoModel) => {
        try {
            await this.post('/api/instituicao/update', model);
        }
        catch (error) {
            throw error;
        }
    }

    public updateInstituicaoCurso = async (id: number | string, model: InstituicaoCursoModel) => {
        try {
            await this.post(`/api/instituicao/${id}/instituicao-curso/update`, model);
        }
        catch (error) {
            throw error;
        }
    }

    public updateInstituicaoCursoOcorrencia = async (id: number | string, idInstituicaoCurso: number | string, model: InstituicaoCursoOcorrenciaModel) => {
        try {
            await this.post(`/api/instituicao/${id}/instituicao-curso/${idInstituicaoCurso}/instituicao-curso-ocorrencia/update`, model);
        }
        catch (error) {
            throw error;
        }
    }

    public saveInstituicaoCursoOcorrenciaNotas = async (id: number | string, notas: Array < InstituicaoCursoOcorrenciaNotaModel > ) => {
        try {
            let result = await this.post(`/api/business/instituicao/instituicao-curso-ocorrencia-notas/${id}/save`, notas);
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public saveFormulaNotaFinal = async (id: number | string, formulaNotaFinal: Array < string > ) => {
        try {
            await this.post(`/api/business/instituicao/formula-nota-final/${id}/save`, formulaNotaFinal);
        }
        catch (error) {
            throw error;
        }
    }

    public formulaNotaFinal = async (id: number | string) => {
        try {
            let result = await this.get(`/api/business/instituicao/formula-nota-final/${id}`) as Array < string > ;
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

    public allInstituicaoCursoOcorrenciaPeriodoAlunoByInstituicaoCursoOCorrenciaPeriodoProfessor = async (id: number | string) => {
        try {
            let result = await this.get(`api/business/instituicao/instituicao-curso-ocorrencia-periodo-aluno/by-instituicao-curso-ocorrencia-periodo-professor/${id}`) as Array < InstituicaoCursoOcorrenciaPeriodoAlunoModel > ;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public allInstituicaoCursoOcorrenciaNotasByInstituicaoCursoOCorrenciaPeriodoProfessor = async (id: number | string) => {
        try {
            let result = await this.get(`api/business/instituicao/instituicao-curso-ocorrencia-notas/by-instituicao-curso-ocorrencia-periodo-professor/${id}`) as Array < InstituicaoCursoOcorrenciaNotaModel > ;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public allInstituicaoCursoOcorrenciaPeriodoByProfessor = async (id: number | string) => {
        try {
            let result = await this.get(`/all-instituicao-curso-ocorrencia-periodo-by-professor/${id}`) as Array < InstituicaoCursoOcorrenciaPeriodoModel > ;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public allInstituicaoBusinessAulaByProfessor = async (id: number | string) => {
        try {
            let result = await this.get(`api/business/instituicao/instituicao-business-aula/by-professor/${id}`) as Array < InstituicaoBusinessAulaModel > ;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public allInstituicaoBusinessAulaByAluno = async (id: number | string) => {
        try {
            let result = await this.get(`api/business/instituicao/instituicao-business-aula/by-aluno/${id}`) as Array < InstituicaoBusinessAulaDetalheAlunoModel > ;
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