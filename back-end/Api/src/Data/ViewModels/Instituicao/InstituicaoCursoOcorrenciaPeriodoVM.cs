using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Interface;

namespace Api.Data.ViewModels {
    public class InstituicaoCursoOcorrenciaPeriodoVM : SelectVM {

        public InstituicaoCursoOcorrenciaPeriodoVM() {
            this.instituicaoCursoOcorrenciaPeriodoProfessores = new List<InstituicaoCursoOcorrenciaPeriodoProfessorVM>();
            this.instituicaoCursoOcorrenciaPeriodoAlunos = new List<InstituicaoCursoOcorrenciaPeriodoAlunoVM>();
        }

        public List<InstituicaoCursoOcorrenciaPeriodoProfessorVM> instituicaoCursoOcorrenciaPeriodoProfessores { get; set; }
        public List<InstituicaoCursoOcorrenciaPeriodoAlunoVM> instituicaoCursoOcorrenciaPeriodoAlunos { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataExpiracao { get; set; }

    }
}