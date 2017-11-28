import { Factory } from './factory';
import { NotifyUtil, MESSAGES, NOTIFY_TYPE } from '../util/notify.util';
import { Instituicao } from '../model/server/instituicao';
import { InstituicaoCurso } from '../model/server/instituicao-curso';
import { InstituicaoCursoPeriodo } from '../model/server/instituicao-curso-periodo';
import { InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula } from '../model/server/instituicao-curso-ocorrencia-periodo-professor-periodo-aula';
import { InstituicaoCursoTurma } from '../model/server/instituicao-curso-turma';
import { CursoGradeMateria } from '../model/server/curso-grade-materia';
import { InstituicaoCursoOcorrencia } from '../model/server/instituicao-curso-ocorrencia';
export class InstituicaoFactory extends Factory {

    private static title = 'Instituicao';

    public static async add(model: Instituicao, notify?: boolean) {
        try {
            let result = await this.put('/api/instituicao/add', model) as Instituicao;
            NotifyUtil.notify(MESSAGES.REGISTRO_ADD, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_ADD_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async addInstituicaoCurso(id: string, model: InstituicaoCurso, notify?: boolean) {
        try {
            let result = await this.put(`/api/instituicao/${id}/instituicao-curso/add`, model) as InstituicaoCurso;
            NotifyUtil.notify(MESSAGES.REGISTRO_ADD, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_ADD_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async addInstituicaoCursoOcorrencia(id: string, idInstituicaoCurso: string, model: InstituicaoCursoOcorrencia, notify?: boolean) {
        try {
            let result = await this.put(`/api/instituicao/${id}/instituicao-curso/${idInstituicaoCurso}/instituicao-curso-ocorrencia/add`, model) as InstituicaoCursoOcorrencia;
            NotifyUtil.notify(MESSAGES.REGISTRO_ADD, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_ADD_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async update(model: Instituicao, notify?: boolean) {
        try {
            let result = await this.post('/api/instituicao/update', model) as Instituicao;
            NotifyUtil.notify(MESSAGES.REGISTRO_UPD, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_UPD_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async updateInstituicaoCurso(id: string, model: InstituicaoCurso, notify?: boolean) {
        try {
            let result = await this.post(`/api/instituicao/${id}/instituicao-curso/update`, model) as InstituicaoCurso;
            NotifyUtil.notify(MESSAGES.REGISTRO_ADD, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_ADD_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async detail(id: string, notify?: boolean) {
        try {
            let result = await this.get(`/api/instituicao/${id}`) as Instituicao;
            NotifyUtil.notify(MESSAGES.REGISTRO_GET, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_GET_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async detailInstituicaoCurso(id: string, idInstituicaoCurso: string, notify?: boolean) {
        try {
            let result = await this.get(`/api/instituicao/${id}/instituicao-curso/${idInstituicaoCurso}`) as InstituicaoCurso;
            NotifyUtil.notify(MESSAGES.REGISTRO_GET, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_GET_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async detailInstituicaoCursoOcorrencia(id: string, idInstituicaoCurso: string, idInstituicaoCursoOcorrencia: string, notify?: boolean) {
        try {
            let result = await this.get(`/api/instituicao/${id}/instituicao-curso/${idInstituicaoCurso}/instituicao-curso-ocorrencia/${idInstituicaoCursoOcorrencia}`) as InstituicaoCursoOcorrencia;
            NotifyUtil.notify(MESSAGES.REGISTRO_GET, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_GET_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async all(notify?: boolean) {
        try {
            let result = await this.get('/api/instituicao') as Array < Instituicao > ;
            NotifyUtil.notify(MESSAGES.REGISTRO_GET, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_GET_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async allCurso(id: string, notify?: boolean) {
        try {
            let result = await this.get(`/api/instituicao/${id}/instituicao-curso`) as Array < InstituicaoCurso > ;
            NotifyUtil.notify(MESSAGES.REGISTRO_GET, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_GET_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async allInstituicaoCursoPeriodo(id: string, idInstituicaoCurso: string, notify?: boolean) {
        try {
            let result = await this.get(`/api/instituicao/${id}/instituicao-curso/${idInstituicaoCurso}/instituicao-curso-periodo`) as Array < InstituicaoCursoPeriodo > ;
            NotifyUtil.notify(MESSAGES.REGISTRO_GET, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_GET_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async allInstituicaoCursoTurma(id: string, idInstituicaoCurso: string, notify?: boolean) {
        try {
            let result = await this.get(`/api/instituicao/${id}/instituicao-curso/${idInstituicaoCurso}/instituicao-curso-turma`) as Array < InstituicaoCursoTurma > ;
            NotifyUtil.notify(MESSAGES.REGISTRO_GET, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_GET_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async allCursoGradeMaterias(id: string, idInstituicaoCurso: string, notify?: boolean) {
        try {
            let result = await this.get(`/api/instituicao/${id}/instituicao-curso/${idInstituicaoCurso}/curso-grade-materia`) as Array < CursoGradeMateria > ;
            NotifyUtil.notify(MESSAGES.REGISTRO_GET, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_GET_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async allInstituicaoCursoOcorrencia(id: string, idInstituicaoCurso: string, notify?: boolean) {
        try {
            let result = await this.get(`/api/instituicao/${id}/instituicao-curso/${idInstituicaoCurso}/instituicao-curso-ocorrencia`) as Array < InstituicaoCursoOcorrencia > ;
            NotifyUtil.notify(MESSAGES.REGISTRO_GET, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_GET_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    public static async disable(id: string, notify?: boolean) {
        try {
            let result = await this.delete('/api/instituicao/disable', { params: { id: id } });
            NotifyUtil.notify(MESSAGES.REGISTRO_DEL, this.title, NOTIFY_TYPE.SUCCESS, !notify);
            return result;
        }
        catch (error) {
            NotifyUtil.notify(MESSAGES.REGISTRO_DEL_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
            throw error;
        }
    }

    // public static async updateCursoOcorrencia(id: string, idInstituicaoCurso: string, dataInicio: string, model: InstituicaoCursoOcorrencia, notify?: boolean) {
    //     try {
    //         let result = await this.post(`/api/instituicao/${id}/instituicao-curso/${idInstituicaoCurso}/${dataInicio}/ocorrencia/update`, model) as InstituicaoCursoOcorrencia;
    //         NotifyUtil.notify(MESSAGES.REGISTRO_ADD, this.title, NOTIFY_TYPE.SUCCESS, !notify);
    //         return result;
    //     }
    //     catch (error) {
    //         NotifyUtil.notify(MESSAGES.REGISTRO_ADD_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
    //         throw error;
    //     }
    // }

    // public static async disableCursoOcorrencia(id: string, idInstituicaoCurso: string, dataInicio: string, model: InstituicaoCursoOcorrencia, notify?: boolean) {
    //     try {
    //         let result = await this.post(`/api/instituicao/${id}/instituicao-curso/${idInstituicaoCurso}/${dataInicio}/ocorrencia/disable`, model) as InstituicaoCursoOcorrencia;
    //         NotifyUtil.notify(MESSAGES.REGISTRO_ADD, this.title, NOTIFY_TYPE.SUCCESS, !notify);
    //         return result;
    //     }
    //     catch (error) {
    //         NotifyUtil.notify(MESSAGES.REGISTRO_ADD_FAIL, this.title, NOTIFY_TYPE.ERROR, !notify);
    //         throw error;
    //     }
    // }


}