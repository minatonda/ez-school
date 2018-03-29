using System;
using System.Collections.Generic;

namespace Api.Common.Base {

    public static class BaseExceptionField {

        public static readonly string BASE_ROLE = "baseRole";
        public static readonly string INSTITUICAO_ROLE = "instituicaoRole";

        public static readonly string USUARIO_USERNAME = "usuario.username";
        public static readonly string USUARIO_PASSWORD = "usuario.password";

        public static readonly string USUARIO_INFO_NOME = "usuarioInfo.nome";
        public static readonly string USUARIO_INFO_DATA_NASCIMENTO = "usuarioInfo.dataNascimento";
        public static readonly string USUARIO_INFO_RG = "usuarioInfo.rg";
        public static readonly string USUARIO_INFO_CPF = "usuarioInfo.cpf";
        public static readonly string USUARIO_INFO_EMAIL = "usuarioInfo.email";
        public static readonly string USUARIO_INFO_TELEFONE = "usuarioInfo.telefone";

        public static readonly string CURSO_NOME = "curso.nome";
        public static readonly string CURSO_DESCRICAO = "curso.descricao";

        public static readonly string INSTITUICAO_NOME = "instituicao.nome";
        public static readonly string INSTITUICAO_CNPJ = "instituicao.cnpj";


        public static readonly string INSTITUICAO_CURSO_CURSO = "instituicaoCurso.curso";
        public static readonly string INSTITUICAO_CURSO_CURSO_GRADE = "instituicaoCurso.cursoGrade";
        public static readonly string INSTITUICAO_CURSO_DATA_INICIO = "instituicaoCurso.dataInicio";

        public static readonly string INSTITUICAO_CURSO_PERIODO_DIA_SEMANA = "instituicaoCursoPeriodo.diaSemana";
        public static readonly string INSTITUICAO_CURSO_PERIODO_INICIO = "instituicaoCursoPeriodo.inicio";
        public static readonly string INSTITUICAO_CURSO_PERIODO_PAUSA_INICIO = "instituicaoCursoPeriodo.pausaInicio";
        public static readonly string INSTITUICAO_CURSO_PERIODO_PAUSA_FIM = "instituicaoCursoPeriodo.pausaFim";
        public static readonly string INSTITUICAO_CURSO_PERIODO_FIM = "instituicaoCursoPeriodo.fim";

        public static readonly string INSTITUICAO_CURSO_TURMA_NOME = "instituicaoCursoTurma.nome";

        public static readonly string CURSO_GRADE_MATERIA_NOME_EXIBICAO = "cursoGradeMateria.nomeExibicao";
        public static readonly string CURSO_GRADE_MATERIA_DESCRICAO = "cursoGradeMateria.Descricao";

        public static readonly string MATERIA_NOME = "materia.nome";
        public static readonly string PROFESSOR_ID = "professor.id";
        public static readonly string ALUNO_ID = "aluno.id";

    }


}