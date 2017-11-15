using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Interface;

namespace Api.Data.ViewModels {
    public class InstituicaoCursoOcorrenciaPeriodoProfessorVM : SelectVM {
        public InstituicaoCursoOcorrenciaPeriodoProfessorVM() {

        }

        public ProfessorVM Professor { get; set; }
        public CursoGradeMateriaVM CursoGradeMateria { get; set; }
        public InstituicaoCursoTurmaVM InstituicaoCursoTurma { get; set; }
        public InstituicaoCursoPeriodoVM InstituicaoCursoPeriodo { get; set; }
        public List<InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaVM> InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas { get; set; }

    }
}