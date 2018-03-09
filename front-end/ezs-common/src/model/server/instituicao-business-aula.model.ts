import { BaseModel } from './base.model';
import { InstituicaoModel } from './instituicao.model';
import { CursoModel } from './curso.model';
import { UsuarioInfoModel } from './usuario-info.model';

export class InstituicaoBusinessAulaModel extends BaseModel < number > {

    idInstituicaoCursoOcorrencia: number;
    idInstituicaoCursoOcorrenciaPeriodoProfessor: number;
    instituicao: InstituicaoModel;
    periodoSequencia: number;
    dataInicio: string;
    dataExpiracao: string;
    curso: CursoModel;
    professor: UsuarioInfoModel;

}