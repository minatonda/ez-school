import { HomeComponent } from '../../components/home/home';
import { UsuarioComponent } from '../../components/usuario/usuario';
import { UsuarioAddUpdComponent } from '../../components/usuario/add-upd/usuario-add-upd';
import { UsuarioAutenticacaoComponent } from '../../components/usuario/usuario-autenticacao/usuario-autenticacao';
import { CursoComponent } from '../../components/curso/curso';
import { CursoAddUpdComponent } from '../../components/curso/add-upd/curso-add-upd';
import { MateriaComponent } from '../../components/materia/materia';
import { MateriaAddUpdComponent } from '../../components/materia/add-upd/materia-add-upd';
import { InstituicaoComponent } from '../../components/instituicao/instituicao';
import { InstituicaoAddUpdComponent } from '../../components/instituicao/add-upd/instituicao-add-upd';
import { InstituicaoCategoriaComponent } from '../../components/instituicao-categoria/instituicao-categoria';
import { InstituicaoCategoriaAddUpdComponent } from '../../components/instituicao-categoria/add-upd/instituicao-categoria-add-upd';
import { RouterConfig, RouterPathType, RouterPath } from '../router/router.path';

export const ROUTER_CONFIGS_CONSTANT: Array<RouterConfig> = [
    { type: RouterPathType.otr, path: RouterPath.ROOT, name: RouterPath.ROOT, component: HomeComponent, alias: 'Home' },

    { type: RouterPathType.list, path: RouterPath.USUARIO, name: RouterPath.USUARIO, component: UsuarioComponent, alias: 'Usuários' },
    { type: RouterPathType.add, path: RouterPath.USUARIO_ADD, name: RouterPath.USUARIO_ADD, component: UsuarioAddUpdComponent, alias: 'Usuário - Adicionar' },
    { type: RouterPathType.upd, path: RouterPath.USUARIO_UPD, name: RouterPath.USUARIO_UPD, component: UsuarioAddUpdComponent, alias: 'Usuário - Atualizar' },
    { type: RouterPathType.nsel, path: RouterPath.USUARIO_AUTENTICACAO, name: RouterPath.USUARIO_AUTENTICACAO, component: UsuarioAutenticacaoComponent, alias: 'Autenticação' },

    { type: RouterPathType.list, path: RouterPath.CURSO, name: RouterPath.CURSO, component: CursoComponent, alias: 'Cursos' },
    { type: RouterPathType.add, path: RouterPath.CURSO_ADD, name: RouterPath.CURSO_ADD, component: CursoAddUpdComponent, alias: 'Curso - Adicionar' },
    { type: RouterPathType.upd, path: RouterPath.CURSO_UPD, name: RouterPath.CURSO_UPD, component: CursoAddUpdComponent, alias: 'Curso - Atualizar' },

    { type: RouterPathType.list, path: RouterPath.MATERIA, name: RouterPath.MATERIA, component: MateriaComponent, alias: 'Materias' },
    { type: RouterPathType.add, path: RouterPath.MATERIA_ADD, name: RouterPath.MATERIA_ADD, component: MateriaAddUpdComponent, alias: 'Materia - Adicionar' },
    { type: RouterPathType.upd, path: RouterPath.MATERIA_UPD, name: RouterPath.MATERIA_UPD, component: MateriaAddUpdComponent, alias: 'Materia - Atualizar' },

    { type: RouterPathType.list, path: RouterPath.INSTITUICAO, name: RouterPath.INSTITUICAO, component: InstituicaoComponent, alias: 'Instituições' },
    { type: RouterPathType.add, path: RouterPath.INSTITUICAO_ADD, name: RouterPath.INSTITUICAO_ADD, component: InstituicaoAddUpdComponent, alias: 'Instituição - Adicionar' },
    { type: RouterPathType.upd, path: RouterPath.INSTITUICAO_UPD, name: RouterPath.INSTITUICAO_UPD, component: InstituicaoAddUpdComponent, alias: 'Instituição - Atualizar' },

    { type: RouterPathType.list, path: RouterPath.INSTITUICAO_CATEGORIA, name: RouterPath.INSTITUICAO_CATEGORIA, component: InstituicaoCategoriaComponent, alias: 'Instituição - Categorias' },
    { type: RouterPathType.add, path: RouterPath.INSTITUICAO_CATEGORIA_ADD, name: RouterPath.INSTITUICAO_CATEGORIA_ADD, component: InstituicaoCategoriaAddUpdComponent, alias: 'Instituição Categoria - Adicionar' },
    { type: RouterPathType.upd, path: RouterPath.INSTITUICAO_CATEGORIA_UPD, name: RouterPath.INSTITUICAO_CATEGORIA_UPD, component: InstituicaoCategoriaAddUpdComponent, alias: 'Instituição Categoria - Atualizar' }

];
