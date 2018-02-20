import { Vue } from 'vue-property-decorator';

export enum AppBroadcastEvent {
    EXIBIR_LOADER = 'EXIBIR_LOADER',
    ESCONDER_LOADER = 'ESCONDER_LOADER',
    AUTENTICADO = 'AUTENTICADO',
    DESAUTENTICADO = 'DESAUTENTICADO',
}

export const AppBroadcastEventBus = new Vue();
