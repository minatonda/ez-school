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
    UNAUTHORIZED = 'UNAUTHORIZED',
    UNAUTHORIZED_INSTITUICAO = 'UNAUTHORIZED_INSTITUICAO',
    RESOURCE_REFUSED = 'RESOURCE_REFUSED',
    FIELD_INVALID = 'FIELD_INVALID',
    FIELD_REQUIRED = 'FIELD_REQUIRED',
    USER_WRONG_USERNAME = 'USER_WRONG_USERNAME',
    USER_WRONG_PASSWORD = 'USER_WRONG_PASSWORD',
    REGISTER_WITH_SAME_VALUE_EXISTS = 'REGISTER_WITH_SAME_VALUE_EXISTS',
}

export enum I18N_ERROR_SERVER_FIELD {
    BASE_ROLE = 'baseRole',
    INSTITUICAO_ROLE = 'instituicaoRole',
    USUARIO_USERNAME = 'usuario.username',
    USUARIO_PASSWORD = 'usuario.password',
    USUARIO_USUARIO_INFO_NOME = 'usuario.usuarioInfo.nome',
    USUARIO_USUARIO_INFO_DATA_NASCIMENTO = 'usuario.usuarioInfo.dataNascimento',
    USUARIO_USUARIO_INFO_EMAIL = 'usuario.usuarioInfo.email',
    USUARIO_USUARIO_INFO_TELEFONE = 'usuario.usuarioInfo.telefone',
    USUARIO_USUARIO_INFO_RG = 'usuario.usuarioInfo.rg',
    USUARIO_USUARIO_INFO_CPF = 'usuario.usuarioInfo.cpf',
    PROFESSOR_ID = 'professor.id',
    ALUNO_ID = 'aluno.id',
}

export enum I18N_LANG {
    ptBR = 0
}

export const I18N_TEMPLATE_MESSAGE_GENERIC_CONSTANT = [
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_GENERIC.AUTENTICAR, title: 'Autenticado', message: 'Bem vindo' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_GENERIC.AUTENTICAR_FALHA, title: 'Falha', message: 'Verifique usuário e senha, ou entre em contato com o administrador.' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_GENERIC.DESAUTENTICAR, title: '', message: '' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_GENERIC.DESAUTENTICAR_FALHA, title: 'Não foi possível efetuar logout', message: 'Entre em contato com o administrador.' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_GENERIC.MODELO_SALVAR, title: 'Êxito', message: 'Registro salvo com êxito.' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_GENERIC.MODELO_SALVAR_FALHA, title: 'Falha', message: 'Falha ao salvar registro.' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_GENERIC.MODELO_DESATIVAR, title: 'Êxito', message: 'Registro desativado com êxito.' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_GENERIC.MODELO_DESATIVAR_FALHA, title: 'Falha ao desativar registro', message: '' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_GENERIC.CONSULTAR, title: 'Êxito', message: 'Registro obtido com êxito.' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_GENERIC.CONSULTAR_FALHA, title: 'Falha', message: 'Falha ao obter registro.' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_GENERIC.ACESSO_NEGADO, title: 'Acesso negado', message: 'Você não possui acesso ao recurso.' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_GENERIC.JA_AUTENTICADO, title: 'Falha', message: 'Você já está autenticado.' },
];

export const I18N_TEMPLATE_MESSAGE_SERVER_CONSTANT = [
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER.UNAUTHORIZED, title: 'Acesso negado', message: 'Você não possui acesso ao recurso.' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER.UNAUTHORIZED_INSTITUICAO, title: 'Acesso negado', message: 'Você não possui acesso ao recurso.' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER.USER_WRONG_USERNAME, title: 'Nome de usuário inválido', message: 'O usuário não existe.' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER.USER_WRONG_PASSWORD, title: 'Senha inválida', message: 'A senha digita é inválida.' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER.FIELD_REQUIRED, title: 'Campo Obrigatório', message: 'O campo {{field}} é obrigatório.' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER.FIELD_INVALID, title: 'Campo Inválido', message: 'O valor {{value}} fornecido para o campo {{field}} é inválido.' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER.RESOURCE_REFUSED, title: 'Recurso Recusado', message: 'O Recurso enviado é inválido.' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER.REGISTER_WITH_SAME_VALUE_EXISTS, title: 'Registro já existente', message: 'O Registro com o campo {{field}} com o mesmo valor {{value}} já existe.' },
];

export const I18N_FIELD_LABELS_CONSTANT = [
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.ALUNO_ID, label: 'ID' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.PROFESSOR_ID, label: 'ID' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.BASE_ROLE, label: 'Regra Base' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.INSTITUICAO_ROLE, label: 'Regra de Instituição' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.USUARIO_USERNAME, label: 'Nome de Usuário' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.USUARIO_PASSWORD, label: 'Senha' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.USUARIO_USUARIO_INFO_NOME, label: 'Nome' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.USUARIO_USUARIO_INFO_DATA_NASCIMENTO, label: 'Data de Nascimento' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.USUARIO_USUARIO_INFO_EMAIL, label: 'Email' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.USUARIO_USUARIO_INFO_TELEFONE, label: 'Telefone' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.USUARIO_USUARIO_INFO_RG, label: 'RG' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.USUARIO_USUARIO_INFO_CPF, label: 'CPF' },
];

export const I18N_ENUM_LABELS_CONSTANTS = [
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Controle Total', value: 'ADMIN' },

    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Adicionar Matéria', value: 'ADD_MATERIA' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Editar Matéria', value: 'EDIT_MATERIA' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Desativar Matéria', value: 'DISABLE_MATERIA' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Detalhe Matéria', value: 'DETAIL_MATERIA' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Listar Matéria', value: 'LIST_MATERIA' },

    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Adicionar Usuário', value: 'ADD_USUARIO' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Editar Usuário', value: 'EDIT_USUARIO' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Desativar Usuário', value: 'DISABLE_USUARIO' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Detalhe Usuário', value: 'DETAIL_USUARIO' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Listar Usuário', value: 'LIST_USUARIO' },

    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Adicionar Curso', value: 'ADD_CURSO' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Editar Curso', value: 'EDIT_CURSO' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Desativar Curso', value: 'DISABLE_CURSO' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Detalhe Curso', value: 'DETAIL_CURSO' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Listar Curso', value: 'LIST_CURSO' },

    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Adicionar Categoria Profissional', value: 'ADD_CATEGORIA_PROFISSIONAL' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Editar Categoria Profissional', value: 'EDIT_CATEGORIA_PROFISSIONAL' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Desativar Categoria Profissional', value: 'DISABLE_CATEGORIA_PROFISSIONAL' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Detalhe Categoria Profissional', value: 'DETAIL_CATEGORIA_PROFISSIONAL' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Listar Categoria Profissional', value: 'LIST_CATEGORIA_PROFISSIONAL' },

    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Adicionar Instituição', value: 'ADD_INSTITUICAO' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Editar Instituição', value: 'EDIT_INSTITUICAO' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Desativar Instituição', value: 'DISABLE_INSTITUICAO' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Detalhe Instituição', value: 'DETAIL_INSTITUICAO' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Listar Instituição', value: 'LIST_INSTITUICAO' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Administração Total de Instituição', value: 'OWNER_INSTITUICAO' },

    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Adicionar Cursos por Instituiçao', value: 'ADD_INSTITUICAO_CURSO' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Editar Cursos por Instituiçao', value: 'EDIT_INSTITUICAO_CURSO' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Desativar Cursos por Instituiçao', value: 'DISABLE_INSTITUICAO_CURSO' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Detalhe Cursos por Instituiçao', value: 'DETAIL_INSTITUICAO_CURSO' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Listar Cursos por Instituiçao', value: 'LIST_INSTITUICAO_CURSO' },

    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Adicionar Colaboradores por Instituiçao', value: 'ADD_INSTITUICAO_COLABORADOR' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Editar Colaboradores por Instituiçao', value: 'EDIT_INSTITUICAO_COLABORADOR' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Desativar Colaboradores por Instituiçao', value: 'DISABLE_INSTITUICAO_COLABORADOR' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Detalhe Colaboradores por Instituiçao', value: 'DETAIL_INSTITUICAO_COLABORADOR' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Listar Colaboradores por Instituiçao', value: 'LIST_INSTITUICAO_COLABORADOR' },

    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Adicionar Perfís de Colaboradores por Instituiçao', value: 'ADD_INSTITUICAO_COLABORADOR_PERFIL' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Editar Perfís de Colaboradores por Instituiçao', value: 'EDIT_INSTITUICAO_COLABORADOR_PERFIL' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Desativar Perfís de Colaboradores por Instituiçao', value: 'DISABLE_INSTITUICAO_COLABORADOR_PERFIL' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Detalhe Perfís de Colaboradores por Instituiçao', value: 'DETAIL_INSTITUICAO_COLABORADOR_PERFIL' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Listar Perfís de Colaboradores por Instituiçao', value: 'LIST_INSTITUICAO_COLABORADOR_PERFIL' },

    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Adicionar Ocorrencias de Cursos por Instituiçao', value: 'ADD_INSTITUICAO_CURSO_OCORRENCIA' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Editar Ocorrencias de Cursos por Instituiçao', value: 'EDIT_INSTITUICAO_CURSO_OCORRENCIA' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Desativar Ocorrencias Cursos de por Instituiçao', value: 'DISABLE_INSTITUICAO_CURSO_OCORRENCIA' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Detalhe Ocorrencias Cursos de por Instituiçao', value: 'DETAIL_INSTITUICAO_CURSO_OCORRENCIA' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Listar Ocorrencias Cursos de por Instituiçao', value: 'LIST_INSTITUICAO_CURSO_OCORRENCIA' },

    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Gerenciar Notas', value: 'EDIT_INSTITUICAO_CURSO_OCORRENCIA_NOTA' },
    { enumerable: ENUM_CONTANT.BASE_ROLE, lang: I18N_LANG.ptBR, label: 'Gerenciar Faltas', value: 'EDIT_INSTITUICAO_CURSO_OCORRENCIA_FALTA' }

];