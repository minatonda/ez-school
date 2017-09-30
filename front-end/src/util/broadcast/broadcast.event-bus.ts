import { Vue } from 'vue-property-decorator';

export enum BroadcastEvent {
    AUTENTICADO = 'AUTENTICADO',
    NOT_AUTENTICADO = 'NOT_AUTENTICADO',
    INICIAR_PAGAMENTO = 'INICIAR_PAGAMENTO',
    EXIBIR_LOADER = 'EXIBIR_LOADER',
    ESCONDER_LOADER = 'ESCONDER_LOADER'
}

export const BroadcastEventBus = new Vue();