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
import { InstituicaoColaboradorModel } from '../../model/server/instituicao-colaborador.model';
import { InstituicaoColaboradorPerfilModel } from '../../model/server/instituicao-colaborador-perfil.model';
import { InstituicaoCursoOcorrenciaAusenciaModel } from '../../model/server/instituicao-curso-ocorrencia-ausencia.model';

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

    public addInstituicaoColaborador = async (id: number | string, model: InstituicaoColaboradorModel) => {
        try {
            await this.put(`/api/instituicao/${id}/instituicao-colaborador/add`, model);
        }
        catch (error) {
            throw error;
        }
    }

    public addInstituicaoColaboradorPerfil = async (id: number | string, model: InstituicaoColaboradorPerfilModel) => {
        try {
            await this.put(`/api/instituicao/${id}/instituicao-colaborador-perfil/add`, model);
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

    public updateInstituicaoColaborador = async (id: number | string, model: InstituicaoColaboradorModel) => {
        try {
            await this.post(`/api/instituicao/${id}/instituicao-colaborador/add`, model);
        }
        catch (error) {
            throw error;
        }
    }

    public updateInstituicaoColaboradorPerfil = async (id: number | string, model: InstituicaoColaboradorPerfilModel) => {
        try {
            await this.post(`/api/instituicao/${id}/instituicao-colaborador-perfil/add`, model);
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

    public saveInstituicaoCursoOcorrenciaNotas = async (id: number | string, notas: Array<InstituicaoCursoOcorrenciaNotaModel>) => {
        try {
            let result = await this.post(`/api/business/instituicao/instituicao-curso-ocorrencia-notas/${id}/save`, notas);
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public saveInstituicaoCursoOcorrenciaAusencias = async (id: number | string, dataAusencia: string, ausencias: Array<InstituicaoCursoOcorrenciaAusenciaModel>) => {
        try {
            let result = await this.post(`/api/business/instituicao/instituicao-curso-ocorrencia-ausencias/${id}/${dataAusencia}/save`, ausencias);
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public saveFormulaNotaFinal = async (id: number | string, formulaNotaFinal: Array<string>) => {
        try {
            await this.post(`/api/business/instituicao/formula-nota-final/${id}/save`, formulaNotaFinal);
        }
        catch (error) {
            throw error;
        }
    }

    public formulaNotaFinal = async (id: number | string) => {
        try {
            let result = await this.get(`/api/business/instituicao/formula-nota-final/${id}`) as Array<string>;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public detail = async (id: number | string) => {
        try {
            let result = await this.get(`/api/instituicao/detail/${id}`) as InstituicaoModel;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public detailInstituicaoColaborador = async (id: number | string, idInstituicaoColaborador: number | string) => {
        try {
            let result = await this.get(`/api/instituicao/detail/${id}/instituicao-colaborador/detail/${idInstituicaoColaborador}`) as InstituicaoColaboradorModel;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public detailInstituicaoColaboradorPerfil = async (id: number | string, idInstituicaoColaboradorPerfil: number | string) => {
        try {
            let result = await this.get(`/api/instituicao/detail/${id}/instituicao-colaborador-perfil/detail/${idInstituicaoColaboradorPerfil}`) as InstituicaoColaboradorPerfilModel;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public detailInstituicaoCurso = async (id: number | string, idInstituicaoCurso: number | string) => {
        try {
            let result = await this.get(`/api/instituicao/detail/${id}/instituicao-curso/detail/${idInstituicaoCurso}`) as InstituicaoCursoModel;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public detailInstituicaoCursoOcorrencia = async (id: number | string, idInstituicaoCurso: number | string, idInstituicaoCursoOcorrencia: number | string) => {
        try {
            let result = await this.get(`/api/instituicao/detail/${id}/instituicao-curso/detail/${idInstituicaoCurso}/instituicao-curso-ocorrencia/detail/${idInstituicaoCursoOcorrencia}`) as InstituicaoCursoOcorrenciaModel;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public all = async () => {
        try {
            let result = await this.get('/api/instituicao') as Array<InstituicaoModel>;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public allInstituicaoColaborador = async (id: number | string) => {
        try {
            let result = await this.get(`/api/instituicao/detail/${id}/instituicao-colaborador`) as Array<InstituicaoColaboradorModel>;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public allInstituicaoColaboradorPerfil = async (id: number | string) => {
        try {
            let result = await this.get(`/api/instituicao/detail/${id}/instituicao-colaborador-perfil`) as Array<InstituicaoColaboradorPerfilModel>;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public allInstituicaoCurso = async (id: number | string) => {
        try {
            let result = await this.get(`/api/instituicao/detail/${id}/instituicao-curso`) as Array<InstituicaoCursoModel>;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public allInstituicaoCursoPeriodo = async (id: number | string, idInstituicaoCurso: number | string) => {
        try {
            let result = await this.get(`/api/instituicao/detail/${id}/instituicao-curso/detail/${idInstituicaoCurso}/instituicao-curso-periodo`) as Array<InstituicaoCursoPeriodoModel>;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public allInstituicaoCursoTurma = async (id: number | string, idInstituicaoCurso: number | string) => {
        try {
            let result = await this.get(`/api/instituicao/detail/${id}/instituicao-curso/detail/${idInstituicaoCurso}/instituicao-curso-turma`) as Array<InstituicaoCursoTurmaModel>;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public allCursoGradeMaterias = async (id: number | string, idInstituicaoCurso: number | string) => {
        try {
            let result = await this.get(`/api/instituicao/detail/${id}/instituicao-curso/detail/${idInstituicaoCurso}/curso-grade-materia`) as Array<CursoGradeMateriaModel>;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public allInstituicaoCursoOcorrencia = async (id: number | string, idInstituicaoCurso: number | string) => {
        try {
            let result = await this.get(`/api/instituicao/detail/${id}/instituicao-curso/detail/${idInstituicaoCurso}/instituicao-curso-ocorrencia`) as Array<InstituicaoCursoOcorrenciaModel>;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public allInstituicaoCursoOcorrenciaPeriodoAlunoByInstituicaoCursoOCorrenciaPeriodoProfessor = async (id: number | string) => {
        try {
            let result = await this.get(`api/business/instituicao/instituicao-curso-ocorrencia-periodo-aluno/by-instituicao-curso-ocorrencia-periodo-professor/${id}`) as Array<InstituicaoCursoOcorrenciaPeriodoAlunoModel>;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public allInstituicaoCursoOcorrenciaNotasByInstituicaoCursoOCorrenciaPeriodoProfessor = async (id: number | string) => {
        try {
            let result = await this.get(`api/business/instituicao/instituicao-curso-ocorrencia-notas/by-instituicao-curso-ocorrencia-periodo-professor/${id}`) as Array<InstituicaoCursoOcorrenciaNotaModel>;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public allInstituicaoCursoOcorrenciaAusenciaByInstituicaoCursoOcorrenciaPeriodoProfessorAndDataAusencia = async (id: number | string, dataAusencia: string) => {
        try {
            let result = await this.get(`api/business/instituicao/instituicao-curso-ocorrencia-ausencias/by-instituicao-curso-ocorrencia-periodo-professor/${id}/${dataAusencia}`) as Array<InstituicaoCursoOcorrenciaAusenciaModel>;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public allInstituicaoBusinessAulaByProfessor = async (id: number | string) => {
        try {
            let result = await this.get(`api/business/instituicao/instituicao-business-aula/by-professor/${id}`) as Array<InstituicaoBusinessAulaModel>;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public allInstituicaoBusinessAulaByAluno = async (id: number | string, emCurso: boolean) => {
        try {
            let result = await this.get(`api/business/instituicao/instituicao-business-aula/by-aluno/${id}`, {
                params: {
                    emCurso: emCurso
                }
            }) as Array<InstituicaoBusinessAulaDetalheAlunoModel>;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public allInstituicaoBusinessAulaByAlunoAndInstituicaoCursoOcorrencia = async (id: number | string, idInstituicaoCursoOcorrencia: number | string) => {
        try {
            let result = await this.get(`api/business/instituicao/instituicao-business-aula/by-aluno-and-instituicao-curso-ocorrencia/${id}/${idInstituicaoCursoOcorrencia}`) as Array<InstituicaoBusinessAulaDetalheAlunoModel>;
            return result;
        }
        catch (error) {
            throw error;
        }
    }

    public allRoles = async () => {
        try {
            let result = await this.get('/api/instituicao/roles') as Array < number > ;
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