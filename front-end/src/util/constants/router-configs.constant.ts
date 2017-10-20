import { HomeComponent } from '../../components/home/home';
import { UsuarioComponent } from '../../components/usuario/usuario';
import { UsuarioManagementComponent } from '../../components/usuario/management/usuario-management';
import { UsuarioAutenticacaoComponent } from '../../components/usuario/autenticacao/usuario-autenticacao';
import { CursoComponent } from '../../components/curso/curso';
import { CursoManagementComponent } from '../../components/curso/management/curso-management';
import { MateriaComponent } from '../../components/materia/materia';
import { MateriaManagementComponent } from '../../components/materia/management/materia-management';
import { InstituicaoComponent } from '../../components/instituicao/instituicao';
import { InstituicaoManagementComponent } from '../../components/instituicao/management/instituicao-management';
import { InstituicaoCategoriaComponent } from '../../components/instituicao-categoria/instituicao-categoria';
import { InstituicaoCategoriaManagementComponent } from '../../components/instituicao-categoria/management/instituicao-categoria-management';
import { RouterConfig, RouterPathType, RouterPath } from '../router/router.path';
import { InstituicaoCursoComponent } from '../../components/instituicao-curso/instituicao-curso';
import { InstituicaoCursoManagementComponent } from '../../components/instituicao-curso/management/instituicao-curso-management';
import { InstituicaoCursoOcorrenciaComponent } from '../../components/instituicao-curso-ocorrencia/instituicao-curso-ocorrencia';
import { InstituicaoCursoOcorrenciaManagementComponent } from '../../components/instituicao-curso-ocorrencia/management/instituicao-curso-ocorrencia-management';
import { UsuarioAlunoComponent } from '../../components/usuario/aluno/usuario-aluno';
import { CategoriaProfissionalComponent } from '../../components/categoria-profissional/categoria-profissional';
import { CategoriaProfissionalManagementComponent } from '../../components/categoria-profissional/management/categoria-profissional-management';

export const ROUTER_CONFIGS_CONSTANT: Array<RouterConfig> = [
    { menu: true, type: RouterPathType.otr, path: RouterPath.ROOT, name: RouterPath.ROOT, component: HomeComponent, alias: 'Home' },

    { menu: true, type: RouterPathType.list, path: RouterPath.USUARIO, name: RouterPath.USUARIO, component: UsuarioComponent, alias: 'Usuários' },
    { menu: true, type: RouterPathType.add, path: RouterPath.USUARIO_ADD, name: RouterPath.USUARIO_ADD, component: UsuarioManagementComponent, alias: 'Usuário' },
    { menu: false, type: RouterPathType.upd, path: RouterPath.USUARIO_UPD, name: RouterPath.USUARIO_UPD, component: UsuarioManagementComponent, alias: 'Usuário' },
    { menu: false, type: RouterPathType.otr, path: RouterPath.USUARIO_ALUNO, name: RouterPath.USUARIO_ALUNO, component: UsuarioAlunoComponent, alias: 'Aluno' },
    { menu: false, type: RouterPathType.otr, path: RouterPath.USUARIO_PROFESSOR, name: RouterPath.USUARIO_PROFESSOR, component: UsuarioAlunoComponent, alias: 'Professor' },
    { menu: false, type: RouterPathType.otr, path: RouterPath.USUARIO_AUTENTICACAO, name: RouterPath.USUARIO_AUTENTICACAO, component: UsuarioAutenticacaoComponent, alias: 'Autenticação' },

    { menu: true, type: RouterPathType.list, path: RouterPath.CURSO, name: RouterPath.CURSO, component: CursoComponent, alias: 'Cursos' },
    { menu: true, type: RouterPathType.add, path: RouterPath.CURSO_ADD, name: RouterPath.CURSO_ADD, component: CursoManagementComponent, alias: 'Curso' },
    { menu: false, type: RouterPathType.upd, path: RouterPath.CURSO_UPD, name: RouterPath.CURSO_UPD, component: CursoManagementComponent, alias: 'Curso' },

    { menu: true, type: RouterPathType.list, path: RouterPath.MATERIA, name: RouterPath.MATERIA, component: MateriaComponent, alias: 'Materias' },
    { menu: true, type: RouterPathType.add, path: RouterPath.MATERIA_ADD, name: RouterPath.MATERIA_ADD, component: MateriaManagementComponent, alias: 'Materia' },
    { menu: false, type: RouterPathType.upd, path: RouterPath.MATERIA_UPD, name: RouterPath.MATERIA_UPD, component: MateriaManagementComponent, alias: 'Materia' },

    { menu: true, type: RouterPathType.list, path: RouterPath.INSTITUICAO, name: RouterPath.INSTITUICAO, component: InstituicaoComponent, alias: 'Instituições' },
    { menu: true, type: RouterPathType.add, path: RouterPath.INSTITUICAO_ADD, name: RouterPath.INSTITUICAO_ADD, component: InstituicaoManagementComponent, alias: 'Instituição' },
    { menu: false, type: RouterPathType.upd, path: RouterPath.INSTITUICAO_UPD, name: RouterPath.INSTITUICAO_UPD, component: InstituicaoManagementComponent, alias: 'Instituição' },

    { menu: true, type: RouterPathType.list, path: RouterPath.INSTITUICAO_CATEGORIA, name: RouterPath.INSTITUICAO_CATEGORIA, component: InstituicaoCategoriaComponent, alias: 'Categorias' },
    { menu: true, type: RouterPathType.add, path: RouterPath.INSTITUICAO_CATEGORIA_ADD, name: RouterPath.INSTITUICAO_CATEGORIA_ADD, component: InstituicaoCategoriaManagementComponent, alias: 'Categoria' },
    { menu: false, type: RouterPathType.upd, path: RouterPath.INSTITUICAO_CATEGORIA_UPD, name: RouterPath.INSTITUICAO_CATEGORIA_UPD, component: InstituicaoCategoriaManagementComponent, alias: 'Categoria' },

    { menu: false, type: RouterPathType.list, path: RouterPath.INSTITUICAO_CURSO, name: RouterPath.INSTITUICAO_CURSO, component: InstituicaoCursoComponent, alias: 'Cursos por Instituição' },
    { menu: false, type: RouterPathType.add, path: RouterPath.INSTITUICAO_CURSO_ADD, name: RouterPath.INSTITUICAO_CURSO_ADD, component: InstituicaoCursoManagementComponent, alias: 'Curso por Instituição' },
    { menu: false, type: RouterPathType.upd, path: RouterPath.INSTITUICAO_CURSO_UPD, name: RouterPath.INSTITUICAO_CURSO_UPD, component: InstituicaoCursoManagementComponent, alias: 'Curso por Instituição' },

    { menu: false, type: RouterPathType.list, path: RouterPath.INSTITUICAO_CURSO_OCORRENCIA, name: RouterPath.INSTITUICAO_CURSO_OCORRENCIA, component: InstituicaoCursoOcorrenciaComponent, alias: 'Ocorrência de Cursos' },
    { menu: false, type: RouterPathType.add, path: RouterPath.INSTITUICAO_CURSO_OCORRENCIA_ADD, name: RouterPath.INSTITUICAO_CURSO_OCORRENCIA_ADD, component: InstituicaoCursoOcorrenciaManagementComponent, alias: 'Ocorrência de Curso' },
    { menu: false, type: RouterPathType.upd, path: RouterPath.INSTITUICAO_CURSO_OCORRENCIA_UPD, name: RouterPath.INSTITUICAO_CURSO_OCORRENCIA_UPD, component: InstituicaoCursoOcorrenciaManagementComponent, alias: 'Ocorrência de Curso' },

    { menu: true, type: RouterPathType.list, path: RouterPath.CATEGORIA_PROFISSIONAL, name: RouterPath.CATEGORIA_PROFISSIONAL, component: CategoriaProfissionalComponent, alias: 'Categoria Profissionais' },
    { menu: true, type: RouterPathType.add, path: RouterPath.CATEGORIA_PROFISSIONAL_ADD, name: RouterPath.CATEGORIA_PROFISSIONAL_ADD, component: CategoriaProfissionalManagementComponent, alias: 'CategoriaProfissional' },
    { menu: false, type: RouterPathType.upd, path: RouterPath.CATEGORIA_PROFISSIONAL_UPD, name: RouterPath.CATEGORIA_PROFISSIONAL_UPD, component: CategoriaProfissionalManagementComponent, alias: 'CategoriaProfissional' }

];
