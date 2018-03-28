import { Vue } from 'vue-property-decorator';

export enum AppBroadcastEvent {
    EXIBIR_LOADER = 'EXIBIR_LOADER',
    ESCONDER_LOADER = 'ESCONDER_LOADER',
    AUTENTICADO = 'AUTENTICADO',
    DESAUTENTICADO = 'DESAUTENTICADO',
    ATIVAR_MODO_PROFESSOR = 'ATIVAR_MODO_PROFESSOR',
    ATIVAR_MODO_ALUNO = 'ATIVAR_MODO_ALUNO'
}

export const AppBroadcastEventBus = new Vue();
