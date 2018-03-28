import { PageListaPropsInterface } from '../page-lista/page-lista-props.interface';
import { CardTableColumn, CardTableMenuEntry } from '../../../../../ezs-common/src/component/card-table/card-table.types';
import { AppRouterPath } from '../../../app.router.path';
import { AppRouter } from '../../../app.router';
import { FACTORY_CONSTANT } from '../../../module/constant/factory.constant';
import { UsuarioModel } from '../../../../../ezs-common/src/model/server/usuario.model';

export class PageUsuarioListaProps implements PageListaPropsInterface {

    columns = [
        new CardTableColumn({
            value: (item: UsuarioModel) => item.usuarioInfo.nome,
            label: () => 'Nome'
        }),
        new CardTableColumn({
            value: (item: UsuarioModel) => item.usuarioInfo.rg,
            label: () => 'RG'
        }),
        new CardTableColumn({
            value: (item: UsuarioModel) => item.username,
            label: () => 'Username'
        }),
        new CardTableColumn({
            value: (item: UsuarioModel) => item.usuarioInfo.email,
            label: () => 'Email'
        }),
        // new CardTableColumn((item: UsuarioModel) => item.usuarioInfo && item.usuarioInfo.perfis.indexOf('ALUNO') > -1 ? '<i class ="fa fa-check">' : '', () => 'Aluno'),
        // new CardTableColumn((item: UsuarioModel) => item.usuarioInfo && item.usuarioInfo.perfis.indexOf('PROFESSOR') > -1 ? '<i class ="fa fa-check">' : '', () => 'Professor'),
        // new CardTableColumn((item: UsuarioModel) => item.usuarioInfo && item.usuarioInfo.perfis.indexOf('ADMINISTRADOR') > -1 ? '<i class ="fa fa-check">' : '', () => 'Administrador'),
    ];
    menu = {
        row: [],
        main: []
    };
    routePathAdd = AppRouterPath.USUARIO_ADD;
    routePathUpdate = AppRouterPath.USUARIO_UPD;
    query = FACTORY_CONSTANT.UsuarioFactory.all;
    queryRemove = FACTORY_CONSTANT.UsuarioFactory.disable;

}