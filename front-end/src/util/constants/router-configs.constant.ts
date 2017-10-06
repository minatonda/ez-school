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

export const ROUTER_CONFIGS_CONSTANT: Array<RouterConfig> = [
    { menu: true, type: RouterPathType.otr, path: RouterPath.ROOT, name: RouterPath.ROOT, component: HomeComponent, alias: 'Home' },

    { menu: true, type: RouterPathType.list, path: RouterPath.USUARIO, name: RouterPath.USUARIO, component: UsuarioComponent, alias: 'Usuários' },
    { menu: true, type: RouterPathType.add, path: RouterPath.USUARIO_ADD, name: RouterPath.USUARIO_ADD, component: UsuarioManagementComponent, alias: 'Usuário - Adicionar' },
    { menu: false, type: RouterPathType.upd, path: RouterPath.USUARIO_UPD, name: RouterPath.USUARIO_UPD, component: UsuarioManagementComponent, alias: 'Usuário - Atualizar' },
    { menu: false, type: RouterPathType.otr, path: RouterPath.USUARIO_AUTENTICACAO, name: RouterPath.USUARIO_AUTENTICACAO, component: UsuarioAutenticacaoComponent, alias: 'Autenticação' },

    { menu: true, type: RouterPathType.list, path: RouterPath.CURSO, name: RouterPath.CURSO, component: CursoComponent, alias: 'Cursos' },
    { menu: true, type: RouterPathType.add, path: RouterPath.CURSO_ADD, name: RouterPath.CURSO_ADD, component: CursoManagementComponent, alias: 'Curso - Adicionar' },
    { menu: false, type: RouterPathType.upd, path: RouterPath.CURSO_UPD, name: RouterPath.CURSO_UPD, component: CursoManagementComponent, alias: 'Curso - Atualizar' },

    { menu: true, type: RouterPathType.list, path: RouterPath.MATERIA, name: RouterPath.MATERIA, component: MateriaComponent, alias: 'Materias' },
    { menu: true, type: RouterPathType.add, path: RouterPath.MATERIA_ADD, name: RouterPath.MATERIA_ADD, component: MateriaManagementComponent, alias: 'Materia - Adicionar' },
    { menu: false, type: RouterPathType.upd, path: RouterPath.MATERIA_UPD, name: RouterPath.MATERIA_UPD, component: MateriaManagementComponent, alias: 'Materia - Atualizar' },

    { menu: true, type: RouterPathType.list, path: RouterPath.INSTITUICAO, name: RouterPath.INSTITUICAO, component: InstituicaoComponent, alias: 'Instituições' },
    { menu: true, type: RouterPathType.add, path: RouterPath.INSTITUICAO_ADD, name: RouterPath.INSTITUICAO_ADD, component: InstituicaoManagementComponent, alias: 'Instituição - Adicionar' },
    { menu: false, type: RouterPathType.upd, path: RouterPath.INSTITUICAO_UPD, name: RouterPath.INSTITUICAO_UPD, component: InstituicaoManagementComponent, alias: 'Instituição - Atualizar' },

    { menu: true, type: RouterPathType.list, path: RouterPath.INSTITUICAO_CATEGORIA, name: RouterPath.INSTITUICAO_CATEGORIA, component: InstituicaoCategoriaComponent, alias: 'Instituição - Categorias' },
    { menu: true, type: RouterPathType.add, path: RouterPath.INSTITUICAO_CATEGORIA_ADD, name: RouterPath.INSTITUICAO_CATEGORIA_ADD, component: InstituicaoCategoriaManagementComponent, alias: 'Instituição Categoria - Adicionar' },
    { menu: false, type: RouterPathType.upd, path: RouterPath.INSTITUICAO_CATEGORIA_UPD, name: RouterPath.INSTITUICAO_CATEGORIA_UPD, component: InstituicaoCategoriaManagementComponent, alias: 'Instituição Categoria - Atualizar' },

    { menu: false, type: RouterPathType.list, path: RouterPath.INSTITUICAO_CURSO, name: RouterPath.INSTITUICAO_CURSO, component: InstituicaoCursoComponent, alias: 'Instituição - Cursos' },
    { menu: false, type: RouterPathType.add, path: RouterPath.INSTITUICAO_CURSO_ADD, name: RouterPath.INSTITUICAO_CURSO_ADD, component: InstituicaoCursoManagementComponent, alias: 'Instituição Cursos - Adicionar' },
    { menu: false, type: RouterPathType.upd, path: RouterPath.INSTITUICAO_CURSO_UPD, name: RouterPath.INSTITUICAO_CURSO_UPD, component: InstituicaoCursoManagementComponent, alias: 'Instituição Cursos - Atualizar' }

];
