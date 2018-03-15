import { PageListaPropsInterface } from '../page-lista/page-lista-props.interface';
import { CardTableColumn, CardTableMenuEntry } from '../../../../../ezs-common/src/component/card-table/card-table.types';
import { AppRouterPath } from '../../../app.router.path';
import { AppRouter } from '../../../app.router';
import { FACTORY_CONSTANT } from '../../../module/constant/factory.constant';
import { InstituicaoModel } from '../../../../../ezs-common/src/model/server/instituicao.model';

export class PageInstituicaoListaProps implements PageListaPropsInterface {

    columns = [
        new CardTableColumn({
            value: (item: InstituicaoModel) => item.nome,
            label: () => 'Nome'
        }),
        new CardTableColumn({
            value: (item: InstituicaoModel) => item.cnpj,
            label: () => 'CNPJ'
        })
    ];
    menu = {
        row: [
            new CardTableMenuEntry({
                label: (item) => 'Gerenciar Cursos',
                method: (item) => AppRouter.push({ name: AppRouterPath.INSTITUICAO_CURSO, params: { id: item.id } }),
                btnClass: (item) => ['fa', 'fa-book'],
                iconClass: (item) => ['btn-primary']
            }),
            new CardTableMenuEntry({
                label: (item) => 'Gerenciar Colaboradores',
                method: (item) => AppRouter.push({ name: AppRouterPath.INSTITUICAO_COLABORADOR, params: { id: item.id } }),
                btnClass: (item) => ['fa', 'fa-users'],
                iconClass: (item) => ['btn-primary']
            }),
            new CardTableMenuEntry({
                label: (item) => 'Gerenciar Perfis',
                method: (item) => AppRouter.push({ name: AppRouterPath.INSTITUICAO_COLABORADOR_PERFIL, params: { id: item.id } }),
                btnClass: (item) => ['fa', 'fa-cubes'],
                iconClass: (item) => ['btn-primary']
            })
        ],
        main: []
    };
    routePathAdd = AppRouterPath.INSTITUICAO_ADD;
    routePathUpdate = AppRouterPath.INSTITUICAO_UPD;
    query = FACTORY_CONSTANT.InstituicaoFactory.all;
    queryRemove = FACTORY_CONSTANT.InstituicaoFactory.disable;

}