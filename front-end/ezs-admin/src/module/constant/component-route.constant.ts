import { AppRouter } from '../../app.router';
import { BaseRouteConfig } from '../../../../ezs-common/src/model/client/base-route-config.model';
import { RouterPathType } from '../../../../ezs-common/src/model/client/router-path-type.model';
import { AppRouterPath } from '../../app.router.path';
import { PageListaComponent } from '../../components/page/page-lista/index';
import { PageHomeComponent } from '../../components/page/page-home/index';
import { PageUsuarioListaProps } from '../../components/page/page-usuario/page-usuario.lista-props';
import { PageUsuarioAutenticacaoComponent } from '../../components/page/page-usuario-autenticacao/index';
import { PageUsuarioComponent } from '../../components/page/page-usuario/index';
import { PageUsuarioAlunoComponent } from '../../components/page/page-usuario-aluno/index';
import { PageUsuarioProfessorComponent } from '../../components/page/page-usuario-professor/index';
import { PageCursoComponent } from '../../components/page/page-curso/index';
import { PageCursoListaProps } from '../../components/page/page-curso/page-curso.lista-props';
import { PageMateriaComponent } from '../../components/page/page-materia/index';
import { PageMateriaListaProps } from '../../components/page/page-materia/page-materia.lista-props';
import { PageInstituicaoComponent } from '../../components/page/page-instituicao/index';
import { PageInstituicaoCategoriaListaProps } from '../../components/page/page-instituicao-categoria/page-instituicao-categoria.lista-props';
import { PageInstituicaoCategoriaComponent } from '../../components/page/page-instituicao-categoria/index';
import { PageInstituicaoListaProps } from '../../components/page/page-instituicao/page-instituicao.lista-props';
import { PageInstituicaoCursoComponent } from '../../components/page/page-instituicao-curso/index';
import { PageInstituicaoCursoListaProps } from '../../components/page/page-instituicao-curso/page-instituicao-curso.lista-props';
import { PageInstituicaoCursoOcorrenciaComponent } from '../../components/page/page-instituicao-curso-ocorrencia/index';
import { PageInstituicaoCursoOcorrenciaListaProps } from '../../components/page/page-instituicao-curso-ocorrencia/page-instituicao-curso-ocorrencia.lista-props';
import { PageCategoriaProfissionalListaProps } from '../../components/page/page-categoria-profissional/page-categoria-profissional.lista-props';
import { PageCategoriaProfissionalComponent } from '../../components/page/page-categoria-profissional/index';

export const COMPONENT_ROUTE_CONSTANT: Array<BaseRouteConfig> = [
    { menu: true, type: RouterPathType.otr, path: AppRouterPath.ROOT, name: AppRouterPath.ROOT, component: PageHomeComponent, alias: 'Home' },

    { menu: false, type: RouterPathType.otr, path: AppRouterPath.USUARIO_AUTENTICACAO, name: AppRouterPath.USUARIO_AUTENTICACAO, component: PageUsuarioAutenticacaoComponent, alias: 'Autenticação' },
    { menu: true, type: RouterPathType.list, path: AppRouterPath.USUARIO, name: AppRouterPath.USUARIO, component: PageListaComponent, alias: 'Usuários', props: new PageUsuarioListaProps() },
    { menu: false, type: RouterPathType.add, path: AppRouterPath.USUARIO_ADD, name: AppRouterPath.USUARIO_ADD, component: PageUsuarioComponent, alias: 'Usuário' },
    { menu: false, type: RouterPathType.upd, path: AppRouterPath.USUARIO_UPD, name: AppRouterPath.USUARIO_UPD, component: PageUsuarioComponent, alias: 'Usuário' },
    { menu: false, type: RouterPathType.otr, path: AppRouterPath.USUARIO_ALUNO, name: AppRouterPath.USUARIO_ALUNO, component: PageUsuarioAlunoComponent, alias: 'Aluno' },
    { menu: false, type: RouterPathType.otr, path: AppRouterPath.USUARIO_PROFESSOR, name: AppRouterPath.USUARIO_PROFESSOR, component: PageUsuarioProfessorComponent, alias: 'Professor' },


    { menu: true, type: RouterPathType.list, path: AppRouterPath.CURSO, name: AppRouterPath.CURSO, component: PageListaComponent, alias: 'Cursos', props: new PageCursoListaProps() },
    { menu: false, type: RouterPathType.add, path: AppRouterPath.CURSO_ADD, name: AppRouterPath.CURSO_ADD, component: PageCursoComponent, alias: 'Curso' },
    { menu: false, type: RouterPathType.upd, path: AppRouterPath.CURSO_UPD, name: AppRouterPath.CURSO_UPD, component: PageCursoComponent, alias: 'Curso' },

    { menu: true, type: RouterPathType.list, path: AppRouterPath.MATERIA, name: AppRouterPath.MATERIA, component: PageListaComponent, alias: 'Materias', props: new PageMateriaListaProps() },
    { menu: false, type: RouterPathType.add, path: AppRouterPath.MATERIA_ADD, name: AppRouterPath.MATERIA_ADD, component: PageMateriaComponent, alias: 'Materia' },
    { menu: false, type: RouterPathType.upd, path: AppRouterPath.MATERIA_UPD, name: AppRouterPath.MATERIA_UPD, component: PageMateriaComponent, alias: 'Materia' },

    { menu: true, type: RouterPathType.list, path: AppRouterPath.CATEGORIA_PROFISSIONAL, name: AppRouterPath.CATEGORIA_PROFISSIONAL, component: PageListaComponent, alias: 'Categoria Profissionais', props: new PageCategoriaProfissionalListaProps() },
    { menu: false, type: RouterPathType.add, path: AppRouterPath.CATEGORIA_PROFISSIONAL_ADD, name: AppRouterPath.CATEGORIA_PROFISSIONAL_ADD, component: PageCategoriaProfissionalComponent, alias: 'Categoria Profissional' },
    { menu: false, type: RouterPathType.upd, path: AppRouterPath.CATEGORIA_PROFISSIONAL_UPD, name: AppRouterPath.CATEGORIA_PROFISSIONAL_UPD, component: PageCategoriaProfissionalComponent, alias: 'Categoria Profissional' },

    { menu: true, type: RouterPathType.list, path: AppRouterPath.INSTITUICAO, name: AppRouterPath.INSTITUICAO, component: PageListaComponent, alias: 'Instituições', props: new PageInstituicaoListaProps() },
    { menu: false, type: RouterPathType.add, path: AppRouterPath.INSTITUICAO_ADD, name: AppRouterPath.INSTITUICAO_ADD, component: PageInstituicaoComponent, alias: 'Instituição' },
    { menu: false, type: RouterPathType.upd, path: AppRouterPath.INSTITUICAO_UPD, name: AppRouterPath.INSTITUICAO_UPD, component: PageInstituicaoComponent, alias: 'Instituição' },

    { menu: true, type: RouterPathType.list, path: AppRouterPath.INSTITUICAO_CATEGORIA, name: AppRouterPath.INSTITUICAO_CATEGORIA, component: PageListaComponent, alias: 'Categorias', props: new PageInstituicaoCategoriaListaProps() },
    { menu: false, type: RouterPathType.add, path: AppRouterPath.INSTITUICAO_CATEGORIA_ADD, name: AppRouterPath.INSTITUICAO_CATEGORIA_ADD, component: PageInstituicaoCategoriaComponent, alias: 'Categoria' },
    { menu: false, type: RouterPathType.upd, path: AppRouterPath.INSTITUICAO_CATEGORIA_UPD, name: AppRouterPath.INSTITUICAO_CATEGORIA_UPD, component: PageInstituicaoCategoriaComponent, alias: 'Categoria' },

    { menu: false, type: RouterPathType.list, path: AppRouterPath.INSTITUICAO_CURSO, name: AppRouterPath.INSTITUICAO_CURSO, component: PageListaComponent, alias: 'Cursos por Instituição', props: new PageInstituicaoCursoListaProps() },
    { menu: false, type: RouterPathType.add, path: AppRouterPath.INSTITUICAO_CURSO_ADD, name: AppRouterPath.INSTITUICAO_CURSO_ADD, component: PageInstituicaoCursoComponent, alias: 'Curso por Instituição' },
    { menu: false, type: RouterPathType.upd, path: AppRouterPath.INSTITUICAO_CURSO_UPD, name: AppRouterPath.INSTITUICAO_CURSO_UPD, component: PageInstituicaoCursoComponent, alias: 'Curso por Instituição' },

    { menu: false, type: RouterPathType.list, path: AppRouterPath.INSTITUICAO_CURSO_OCORRENCIA, name: AppRouterPath.INSTITUICAO_CURSO_OCORRENCIA, component: PageListaComponent, alias: 'Ocorrência de Cursos', props: new PageInstituicaoCursoOcorrenciaListaProps() },
    { menu: false, type: RouterPathType.add, path: AppRouterPath.INSTITUICAO_CURSO_OCORRENCIA_ADD, name: AppRouterPath.INSTITUICAO_CURSO_OCORRENCIA_ADD, component: PageInstituicaoCursoOcorrenciaComponent, alias: 'Ocorrência de Curso' },
    { menu: false, type: RouterPathType.upd, path: AppRouterPath.INSTITUICAO_CURSO_OCORRENCIA_UPD, name: AppRouterPath.INSTITUICAO_CURSO_OCORRENCIA_UPD, component: PageInstituicaoCursoOcorrenciaComponent, alias: 'Ocorrência de Curso' },


];