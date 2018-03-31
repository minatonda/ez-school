import { BaseModel } from './base.model';
import { InstituicaoModel } from './instituicao.model';
import { CursoModel } from './curso.model';
import { UsuarioInfoModel } from './usuario-info.model';

export class InstituicaoBusinessAulaModel extends BaseModel < number > {

    idInstituicaoCursoOcorrencia: number = null;
    idInstituicaoCursoOcorrenciaPeriodoProfessor: number = null;
    instituicao: InstituicaoModel = null;
    periodoSequencia: number = null;
    dataInicio: string = null;
    dataExpiracao: string = null;
    curso: CursoModel = null;
    professor: UsuarioInfoModel = null;

}