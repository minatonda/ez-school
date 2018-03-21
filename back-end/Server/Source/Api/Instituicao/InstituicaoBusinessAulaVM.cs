using System;
using Api.Common.Base;
using Api.CursoApi;
using Api.UsuarioApi;

namespace Api.InstituicaoApi {

    public interface IInstituicaoBusinessAulaVM {
        long IdInstituicaoCursoOcorrenciaPeriodoProfessor { get; set; }
        long IdInstituicaoCursoOcorrencia { get; set; }
        long PeriodoSequencia { get; set; }
        DateTime? DataInicio { get; set; }
        DateTime? DataExpiracao { get; set; }
        InstituicaoVM Instituicao { get; set; }
        CursoVM Curso { get; set; }
        UsuarioInfoVM Professor { get; set; }
        CursoGradeMateriaVM Materia { get; set; }
    }
    public class InstituicaoBusinessAulaVM : BaseVM<long>, IInstituicaoBusinessAulaVM {

        public long IdInstituicaoCursoOcorrenciaPeriodoProfessor { get; set; }
        public long IdInstituicaoCursoOcorrencia { get; set; }
        public long PeriodoSequencia { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataExpiracao { get; set; }
        public InstituicaoVM Instituicao { get; set; }
        public CursoVM Curso { get; set; }
        public UsuarioInfoVM Professor { get; set; }
        public CursoGradeMateriaVM Materia { get; set; }

    }
}