import { ENUM_CONTANT } from './enum.contant';

export enum I18N_ERROR_GENERIC {
    AUTENTICAR,
    AUTENTICAR_FALHA,
    DESAUTENTICAR,
    DESAUTENTICAR_FALHA,
    MODELO_SALVAR,
    MODELO_SALVAR_FALHA,
    MODELO_DESATIVAR,
    MODELO_DESATIVAR_FALHA,
    CONSULTAR,
    CONSULTAR_FALHA,
    ACESSO_NEGADO,
    JA_AUTENTICADO
}

export enum I18N_ERROR_SERVER {
    UNAUTHORIZED,
    UNAUTHORIZED_INSTITUICAO,
    INVALID_FIELD
}

export enum I18N_LANG {
    ptBR = 0
}

export const I18N_TEMPLATE_MESSAGE_GENERIC_CONSTANT = [
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_GENERIC.AUTENTICAR, title: 'Registro salvo com êxito !', message: '' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_GENERIC.AUTENTICAR_FALHA, title: 'Falha ao salvar registro !', message: '' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_GENERIC.DESAUTENTICAR, title: 'Autenticação com êxito !', message: '' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_GENERIC.DESAUTENTICAR_FALHA, title: 'Falha ao autenticar !', message: '' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_GENERIC.MODELO_SALVAR, title: 'Registro salvo com êxito !', message: '' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_GENERIC.MODELO_SALVAR_FALHA, title: 'Falha ao salvar êxito !', message: '' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_GENERIC.MODELO_DESATIVAR, title: 'Registro desativado com êxito !', message: '' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_GENERIC.MODELO_DESATIVAR_FALHA, title: 'Falha ao desativar registro', message: '' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_GENERIC.CONSULTAR, title: 'Registro obtido com êxito !', message: '' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_GENERIC.CONSULTAR_FALHA, title: 'Falha ao obter registro', message: '' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_GENERIC.ACESSO_NEGADO, title: 'Acesso negado', message: 'Você não possui acesso ao recurso !' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_GENERIC.JA_AUTENTICADO, title: 'Já Autenticado', message: 'Você já está autenticado !' },
];

export const I18N_TEMPLATE_MESSAGE_SERVER_CONSTANT = [
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER.UNAUTHORIZED, title: 'Acesso negado', message: 'Você não possui acesso ao recurso "{{Role}}"' },

];

export const I18N_ENUM_LABELS_CONSTANTS = [
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Controle Total', value: 0 },

    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Adicionar Matéria', value: 1 },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Editar Matéria', value: 2 },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Desativar Matéria', value: 3 },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Detalhe Matéria', value: 4 },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Listar Matéria', value: 5 },

    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Adicionar Usuário', value: 6 },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Editar Usuário', value: 7 },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Desativar Usuário', value: 8 },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Detalhe Usuário', value: 9 },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Listar Usuário', value: 10 },

    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Adicionar Curso', value: 11 },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Editar Curso', value: 12 },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Desativar Curso', value: 13 },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Detalhe Curso', value: 14 },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Listar Curso', value: 15 },

    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Adicionar Categoria Profissional', value: 16 },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Editar Categoria Profissional', value: 17 },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Desativar Categoria Profissional', value: 18 },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Detalhe Categoria Profissional', value: 19 },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Listar Categoria Profissional', value: 20 },

    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Adicionar Instituição', value: 21 },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Editar Instituição', value: 22 },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Desativar Instituição', value: 23 },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Detalhe Instituição', value: 24 },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Listar Instituição', value: 25 },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Administração Total de Instituição', value: 26 },


    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Adicionar Cursos por Instituiçao', value: 27 },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Editar Cursos por Instituiçao', value: 28 },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Desativar Cursos por Instituiçao', value: 29 },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Detalhe Cursos por Instituiçao', value: 30 },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Listar Cursos por Instituiçao', value: 31 },

    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Adicionar Ocorrencias de Cursos por Instituiçao', value: 32 },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Editar Ocorrencias de Cursos por Instituiçao', value: 33 },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Desativar Ocorrencias Cursos de por Instituiçao', value: 34 },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Detalhe Ocorrencias Cursos de por Instituiçao', value: 35 },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Listar Ocorrencias Cursos de por Instituiçao', value: 36 },

    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Gerenciar Notas', value: 37 },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Gerenciar Faltas', value: 38 }

];