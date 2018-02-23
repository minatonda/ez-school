using System.Collections.Generic;
using Api.Common.Base;
using Api.CursoApi;
using Api.UsuarioApi;

namespace Api.InstituicaoApi {

    public class InstituicaoCursoOcorrenciaPeriodoProfessorVM : BaseVM {
        
        public InstituicaoCursoOcorrenciaPeriodoProfessorVM() {

        }

        public ProfessorVM Professor { get; set; }
        public CursoGradeMateriaVM CursoGradeMateria { get; set; }
        public InstituicaoCursoTurmaVM InstituicaoCursoTurma { get; set; }
        public InstituicaoCursoPeriodoVM InstituicaoCursoPeriodo { get; set; }
        public List<InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaVM> InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas { get; set; }

    }
}