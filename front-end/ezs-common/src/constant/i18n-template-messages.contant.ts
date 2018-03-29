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
    FIELD_HOUR_INVALID = 'FIELD_HOUR_INVALID',
    FIELD_HOUR_MINOR_THAN = 'FIELD_HOUR_MINOR_THAN',
    FIELD_DATE_INVALID = 'FIELD_DATE_INVALID',
    USER_WRONG_USERNAME = 'USER_WRONG_USERNAME',
    USER_WRONG_PASSWORD = 'USER_WRONG_PASSWORD',
    REGISTER_WITH_SAME_VALUE_EXISTS = 'REGISTER_WITH_SAME_VALUE_EXISTS',
}

export enum I18N_ERROR_SERVER_FIELD {

    BASE_ROLE = 'baseRole',
    INSTITUICAO_ROLE = 'instituicaoRole',
    USUARIO_USERNAME = 'usuario.username',
    USUARIO_PASSWORD = 'usuario.password',
    USUARIO_INFO_NOME = 'usuarioInfo.nome',
    USUARIO_INFO_DATA_NASCIMENTO = 'usuarioInfo.dataNascimento',
    USUARIO_INFO_RG = 'usuarioInfo.rg',
    USUARIO_INFO_CPF = 'usuarioInfo.cpf',
    USUARIO_INFO_EMAIL = 'usuarioInfo.email',
    USUARIO_INFO_TELEFONE = 'usuarioInfo.telefone',
    CURSO_NOME = 'curso.nome',
    CURSO_DESCRICAO = 'curso.descricao',
    INSTITUICAO_NOME = 'instituicao.nome',
    INSTITUICAO_CNPJ = 'instituicao.cnpj',
    INSTITUICAO_CURSO_CURSO = 'instituicaoCurso.curso',
    INSTITUICAO_CURSO_CURSO_GRADE = 'instituicaoCurso.cursoGrade',
    INSTITUICAO_CURSO_DATA_INICIO = 'instituicaoCurso.dataInicio',
    INSTITUICAO_CURSO_PERIODO_DIA_SEMANA = 'instituicaoCursoPeriodo.diaSemana',
    INSTITUICAO_CURSO_PERIODO_INICIO = 'instituicaoCursoPeriodo.inicio',
    INSTITUICAO_CURSO_PERIODO_PAUSA_INICIO = 'instituicaoCursoPeriodo.pausaInicio',
    INSTITUICAO_CURSO_PERIODO_PAUSA_FIM = 'instituicaoCursoPeriodo.pausaFim',
    INSTITUICAO_CURSO_PERIODO_FIM = 'instituicaoCursoPeriodo.fim',
    INSTITUICAO_CURSO_TURMA_NOME = 'instituicaoCursoTurma.nome',
    CURSO_GRADE_MATERIA_NOME_EXIBICAO = 'cursoGradeMateria.nomeExibicao',
    CURSO_GRADE_MATERIA_DESCRICAO = 'cursoGradeMateria.Descricao',
    MATERIA_NOME = 'materia.nome',
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
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER.FIELD_DATE_INVALID, title: 'Data inválida', message: 'A data {{value}} fornecida para o campo {{field}} é inválida.' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER.FIELD_HOUR_INVALID, title: 'Hora inválida', message: 'A hora {{value}} fornecida para o campo {{field}} é inválida.' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER.FIELD_HOUR_MINOR_THAN, title: 'Hora inválida', message: 'A hora {{value}} fornecida para o campo {{field}} é menor que o(s) campo(s) : {{references}}' },
];

export const I18N_FIELD_LABELS_CONSTANT = [
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.ALUNO_ID, label: 'Aluno - ID' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.PROFESSOR_ID, label: 'Professor - ID' },

    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.BASE_ROLE, label: 'Regra Base' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.INSTITUICAO_ROLE, label: 'Regra de Instituição' },

    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.USUARIO_USERNAME, label: 'Nome de Usuário (Usuário)' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.USUARIO_PASSWORD, label: 'Senha (Usuário)' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.USUARIO_INFO_NOME, label: 'Nome (Usuário)' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.USUARIO_INFO_DATA_NASCIMENTO, label: 'Data de Nascimento (Usuário)' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.USUARIO_INFO_EMAIL, label: 'Email (Usuário)' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.USUARIO_INFO_TELEFONE, label: 'Telefone (Usuário)' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.USUARIO_INFO_RG, label: 'RG (Usuário)' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.USUARIO_INFO_CPF, label: 'CPF (Usuário)' },

    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.MATERIA_NOME, label: ' - Nome (Matéria)' },

    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.INSTITUICAO_NOME, label: ' - Nome (Instituição)' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.INSTITUICAO_CNPJ, label: ' - CNPJ (Instituição)' },

    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.INSTITUICAO_CURSO_CURSO, label: 'Curso (Curso por Instituição)' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.INSTITUICAO_CURSO_CURSO_GRADE, label: 'Grade (Curso por Instituição)' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.INSTITUICAO_CURSO_DATA_INICIO, label: 'Data de início (Curso por Instituição)' },

    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.INSTITUICAO_CURSO_PERIODO_DIA_SEMANA, label: 'Dias da Semana (Periodo do Curso)' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.INSTITUICAO_CURSO_PERIODO_INICIO, label: 'Início (Periodo do Curso)' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.INSTITUICAO_CURSO_PERIODO_PAUSA_INICIO, label: 'Início da Pausa (Periodo do Curso)' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.INSTITUICAO_CURSO_PERIODO_PAUSA_FIM, label: 'Fim da Pausa (Periodo do Curso)' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.INSTITUICAO_CURSO_PERIODO_FIM, label: 'Fim (Periodo do Curso)' },

    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.INSTITUICAO_CURSO_TURMA_NOME, label: 'Nome (Turma do Curso)' },


    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.CURSO_NOME, label: 'Nome (Curso)' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.CURSO_DESCRICAO, label: 'Descrição (Curso)' },

    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.CURSO_GRADE_MATERIA_NOME_EXIBICAO, label: 'Nome de Exibição (Matéria da Grade de Curso)' },
    { lang: I18N_LANG.ptBR, i18nMessage: I18N_ERROR_SERVER_FIELD.CURSO_GRADE_MATERIA_DESCRICAO, label: 'Descrição (Matéria da Grade de Curso)'  },
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