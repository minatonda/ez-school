export enum I18N_MESSAGE {
    AUTENTICAR = 1,
    AUTENTICAR_FALHA = -1,
    DESAUTENTICAR = 2,
    DESAUTENTICAR_FALHA = -2,
    MODELO_SALVAR = 3,
    MODELO_SALVAR_FALHA = -3,
    MODELO_DESATIVAR = 4,
    MODELO_DESATIVAR_FALHA = -4,
    CONSULTAR = 5,
    CONSULTAR_FALHA = -5
}

export enum I18N_LANG {
    ptBR = 0
}

export const I18N_TEMPLATE_MESSAGE_CONSTANT = [
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_MESSAGE.AUTENTICAR, title: 'Registro salvo com êxito !', message: '' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_MESSAGE.AUTENTICAR_FALHA, title: 'Falha ao salvar registro !', message: '' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_MESSAGE.DESAUTENTICAR, title: 'Autenticação com êxito !', message: '' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_MESSAGE.DESAUTENTICAR_FALHA, title: 'Falha ao autenticar !', message: '' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_MESSAGE.MODELO_SALVAR, title: 'Registro salvo com êxito !', message: '' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_MESSAGE.MODELO_SALVAR_FALHA, title: 'Falha ao salvar êxito !', message: '' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_MESSAGE.MODELO_DESATIVAR, title: 'Registro desativado com êxito !', message: '' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_MESSAGE.MODELO_DESATIVAR_FALHA, title: 'Falha ao desativar registro', message: '' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_MESSAGE.CONSULTAR, title: 'Registro obtido com êxito !', message: '' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_MESSAGE.CONSULTAR_FALHA, title: 'Falha ao obter registro', message: '' },
];