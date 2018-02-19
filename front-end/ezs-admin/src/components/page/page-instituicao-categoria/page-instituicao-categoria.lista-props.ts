import { PageListaPropsInterface } from '../page-lista/page-lista-props.interface';
import { CardTableColumn, CardTableMenuEntry } from '../../../../../ezs-common/src/component/card-table/card-table.types';
import { AppRouterPath } from '../../../app.router.path';
import { AppRouter } from '../../../app.router';
import { Factory } from '../../../module/constant/factory.constant';
import { InstituicaoCategoriaModel } from '../../../../../ezs-common/src/model/server/instituicao-categoria.model';

export class PageInstituicaoCategoriaListaProps implements PageListaPropsInterface {

    columns = [
        new CardTableColumn((item: InstituicaoCategoriaModel) => item.nome, () => 'Nome'),
        new CardTableColumn((item: InstituicaoCategoriaModel) => item.descricao, () => 'Descrição')
    ];
    menu = {
        row: [
            new CardTableMenuEntry(
                (item) => AppRouter.push({ name: AppRouterPath.INSTITUICAO_CURSO, params: { id: item.id } }),
                (item) => 'Gerenciar Cursos',
                (item) => ['fa', 'fa-book'],
                (item) => ['btn-primary']
            )
        ], main: []
    };
    routePathAdd = AppRouterPath.INSTITUICAO_CATEGORIA_ADD;
    routePathUpdate = AppRouterPath.INSTITUICAO_CATEGORIA_UPD;
    query = Factory.InstituicaoCategoriaFactory.all;
    queryRemove = Factory.InstituicaoCategoriaFactory.disable;

}