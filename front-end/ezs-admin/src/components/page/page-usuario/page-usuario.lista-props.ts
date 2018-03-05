import { PageListaPropsInterface } from '../page-lista/page-lista-props.interface';
import { CardTableColumn, CardTableMenuEntry } from '../../../../../ezs-common/src/component/card-table/card-table.types';
import { AppRouterPath } from '../../../app.router.path';
import { AppRouter } from '../../../app.router';
import { Factory } from '../../../module/constant/factory.constant';
import { UsuarioModel } from '../../../../../ezs-common/src/model/server/usuario.model';

export class PageUsuarioListaProps implements PageListaPropsInterface {

    columns = [
        new CardTableColumn((item: UsuarioModel) => item.username, () => 'Username'),
        new CardTableColumn((item: UsuarioModel) => item.usuarioInfo && item.usuarioInfo.nome || '', () => 'Nome'),
        new CardTableColumn((item: UsuarioModel) => item.usuarioInfo && item.usuarioInfo.perfis.indexOf('ALUNO') > -1 ? '<i class ="fa fa-check">' : '', () => 'Aluno'),
        new CardTableColumn((item: UsuarioModel) => item.usuarioInfo && item.usuarioInfo.perfis.indexOf('PROFESSOR') > -1 ? '<i class ="fa fa-check">' : '', () => 'Professor'),
        new CardTableColumn((item: UsuarioModel) => item.usuarioInfo && item.usuarioInfo.perfis.indexOf('ADMINISTRADOR') > -1 ? '<i class ="fa fa-check">' : '', () => 'Administrador'),
    ];
    menu = {
        row: [],
        main: []
    };
    routePathAdd = AppRouterPath.USUARIO_ADD;
    routePathUpdate = AppRouterPath.USUARIO_UPD;
    query = Factory.UsuarioFactory.all;
    queryRemove = Factory.UsuarioFactory.disable;

}