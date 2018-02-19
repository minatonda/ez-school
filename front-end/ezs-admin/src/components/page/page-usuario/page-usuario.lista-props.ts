import { PageListaPropsInterface } from '../page-lista/page-lista-props.interface';
import { CardTableColumn, CardTableMenuEntry } from '../../../../../ezs-common/src/component/card-table/card-table.types';
import { AppRouterPath } from '../../../app.router.path';
import { AppRouter } from '../../../app.router';
import { Factory } from '../../../module/constant/factory.constant';
import { UsuarioModel } from '../../../../../ezs-common/src/model/server/usuario.model';

export class PageUsuarioListaProps implements PageListaPropsInterface {

    columns = [
        new CardTableColumn((item: UsuarioModel) => item.username, () => 'Username'),
        new CardTableColumn((item: UsuarioModel) => item.usuarioInfo && item.usuarioInfo.nome || '', () => 'Nome')
    ];
    menu = {
        row: [
            new CardTableMenuEntry(
                (item) => AppRouter.push({ name: AppRouterPath.USUARIO_ALUNO, params: item }),
                (item) => 'Aluno',
                (item) => ['fa', 'fa-edit'],
                (item) => ['btn-primary']
            ),
            new CardTableMenuEntry(
                (item) => AppRouter.push({ name: AppRouterPath.USUARIO_PROFESSOR, params: item }),
                (item) => 'Professor',
                (item) => ['fa', 'fa-edit'],
                (item) => ['btn-primary']
            )
        ], main: [

        ]
    };
    routePathAdd = AppRouterPath.USUARIO_ADD;
    routePathUpdate = AppRouterPath.USUARIO_UPD;
    query = Factory.UsuarioFactory.all;
    queryRemove = Factory.UsuarioFactory.disable;

}