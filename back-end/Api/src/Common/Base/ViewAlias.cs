using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Common.Base {
    public class ViewAlias {

        public static string ROOT = "/";
        public static string ABOUT = "/about";

        public static string USUARIO_AUTENTICACAO = "/usuario/autenticacao";
        public static string USUARIO = "/usuario";
        public static string USUARIO_ADD = "/usuario/add";
        public static string USUARIO_UPD = "/usuario/:id/upd";
        public static string USUARIO_ALUNO = "/usuario/:id/aluno";
        public static string USUARIO_PROFESSOR = "/usuario/:id/professor";
        public static string USUARIO_ADD_EXTERNAL = "/usuario/cadastro";

        public static string CURSO = "/curso";
        public static string CURSO_ADD = "/curso/add";
        public static string CURSO_UPD = "/curso/:id/upd";

        public static string MATERIA = "/materia";
        public static string MATERIA_ADD = "/materia/add";
        public static string MATERIA_UPD = "/materia/:id/upd";

        public static string INSTITUICAO = "/instituicao";
        public static string INSTITUICAO_ADD = "/instituicao/add";
        public static string INSTITUICAO_UPD = "/instituicao/:id/upd";

        public static string INSTITUICAO_CATEGORIA = "/instituicao-categoria";
        public static string INSTITUICAO_CATEGORIA_ADD = "/instituicao-categoria/add";
        public static string INSTITUICAO_CATEGORIA_UPD = "/instituicao-categoria/:id/upd/";

        public static string INSTITUICAO_CURSO = "/instituicao/:id/curso";
        public static string INSTITUICAO_CURSO_ADD = "/instituicao/:id/curso/add";
        public static string INSTITUICAO_CURSO_UPD = "/instituicao/:id/curso/:idInstituicaoCurso/upd";

        public static string INSTITUICAO_COLABORADOR = "/instituicao/:id/colaborador";
        public static string INSTITUICAO_COLABORADOR_ADD = "/instituicao/:id/colaborador/add";
        public static string INSTITUICAO_COLABORADOR_UPD = "/instituicao/:id/colaborador/:idInstituicaoColaborador/upd";
        public static string INSTITUICAO_COLABORADOR_PERFIL = "/instituicao/:id/colaborador-perfil";
        public static string INSTITUICAO_COLABORADOR_PERFIL_ADD = "/instituicao/:id/colaborador-perfil/add";
        public static string INSTITUICAO_COLABORADOR_PERFIL_UPD = "/instituicao/:id/colaborador-perfil/:idInstituicaoColaboradorPerfil/upd";

        public static string INSTITUICAO_CURSO_OCORRENCIA = "/instituicao/:id/curso/:idInstituicaoCurso/ocorrencia";
        public static string INSTITUICAO_CURSO_OCORRENCIA_ADD = "/instituicao/:id/curso/:idInstituicaoCurso/ocorrencia/add";
        public static string INSTITUICAO_CURSO_OCORRENCIA_UPD = "/instituicao/:id/curso/:idInstituicaoCurso/ocorrencia/:idInstituicaoCursoOcorrencia/upd";

        public static string CATEGORIA_PROFISSIONAL = "/categoria-profissinal";
        public static string CATEGORIA_PROFISSIONAL_ADD = "/categoria-profissinal/add";
        public static string CATEGORIA_PROFISSIONAL_UPD = "/categoria-profissinal/:id/upd";

    }

}