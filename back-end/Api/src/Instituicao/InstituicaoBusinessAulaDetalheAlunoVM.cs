using System;
using Api.Common.Base;
using Api.CursoApi;
using Api.UsuarioApi;

namespace Api.InstituicaoApi {
    public class InstituicaoBusinessAulaDetalheAlunoVM : BaseVM<long>, IInstituicaoBusinessAulaVM {

        public long IdInstituicaoCursoOcorrenciaPeriodoProfessor { get; set; }
        public long IdInstituicaoCursoOcorrencia { get; set; }
        public long PeriodoSequencia { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataExpiracao { get; set; }
        public InstituicaoVM Instituicao { get; set; }
        public CursoVM Curso { get; set; }
        public UsuarioInfoVM Professor { get; set; }
        public CursoGradeMateriaVM Materia { get; set; }
        public double? Nota { get; set; }
        public long Ausencias { get; set; }

    }
}