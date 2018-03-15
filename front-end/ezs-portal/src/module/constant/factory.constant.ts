import { Provider } from '../../../../ezs-common/src/provider';
import { AutenticacaoService } from '../service/autenticacao.service';
export const FACTORY_CONSTANT = Provider.retrieveFactories(AutenticacaoService,
    () => {

    },
    () => {

    }
);