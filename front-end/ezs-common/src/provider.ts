import { AutenticaoServiceInterface } from './service/autenticacao.service.interface';
import { Factory as UsuarioFactory } from './factory/usuario/usuario.factory';
import { Factory as CategoriaProfissionalFactory } from './factory/categoria-profissional/categoria-profissional.factory';
import { Factory as InstituicaoFactory } from './factory/instituicao/instituicao.factory';
import { Factory as InstituicaoCategoriaFactory } from './factory/instituicao/instituicao-categoria.factory';
import { Factory as MateriaFactory } from './factory/materia/materia.factory';
import { Factory as CursoFactory } from './factory/curso/curso.factory';
import { LoaderCompactComponent } from './component/loader-compact/loader-compact';
import { CardTableComponent } from './component/card-table/card-table';
import { SelectorComponent } from './component/selector/selector';
import { DateCatcherComponent } from './component/date-catcher/date-catcher';


export class Provider {
    public static retrieveFactories(autenticacaoService: AutenticaoServiceInterface) {
        return {
            UsuarioFactory: new UsuarioFactory(autenticacaoService),
            CategoriaProfissionalFactory: new CategoriaProfissionalFactory(autenticacaoService),
            InstituicaoFactory: new InstituicaoFactory(autenticacaoService),
            InstituicaoCategoriaFactory: new InstituicaoCategoriaFactory(autenticacaoService),
            MateriaFactory: new MateriaFactory(autenticacaoService),
            CursoFactory: new CursoFactory(autenticacaoService),
        };
    }

    public static retrieveComponents() {
        return [
            { alias: 'loader-compact', component: LoaderCompactComponent },
            { alias: 'card-table', component: CardTableComponent },
            { alias: 'selector', component: SelectorComponent },
            { alias: 'date-catcher', component: DateCatcherComponent }
        ];
    }
}