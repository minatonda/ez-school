import { PageListaPropsInterface } from '../page-lista/page-lista-props.interface';
import { CardTableColumn, CardTableMenuEntry } from '../../../../../ezs-common/src/component/card-table/card-table.types';
import { AppRouterPath } from '../../../app.router.path';
import { AppRouter } from '../../../app.router';
import { FACTORY_CONSTANT } from '../../../module/constant/factory.constant';
import { InstituicaoCategoriaModel } from '../../../../../ezs-common/src/model/server/instituicao-categoria.model';

export class PageInstituicaoCategoriaListaProps implements PageListaPropsInterface {

    columns = [
        new CardTableColumn({
            value: (item: InstituicaoCategoriaModel) => item.nome,
            label: () => 'Nome'
        }),
        new CardTableColumn({
            value: (item: InstituicaoCategoriaModel) => item.descricao,
            label: () => 'Descrição'
        })
    ];
    menu = {
        row: [
            new CardTableMenuEntry({
                label: (item) => 'Gerenciar Cursos',
                method: (item) => AppRouter.push({ name: AppRouterPath.INSTITUICAO_CURSO, params: { id: item.id } }),
                btnClass: (item) => ['fa', 'fa-book'],
                iconClass: (item) => ['btn-primary']
            })
        ],
        main: []
    };
    routePathAdd = AppRouterPath.INSTITUICAO_CATEGORIA_ADD;
    routePathUpdate = AppRouterPath.INSTITUICAO_CATEGORIA_UPD;
    query = FACTORY_CONSTANT.InstituicaoCategoriaFactory.all;
    queryRemove = FACTORY_CONSTANT.InstituicaoCategoriaFactory.disable;

}