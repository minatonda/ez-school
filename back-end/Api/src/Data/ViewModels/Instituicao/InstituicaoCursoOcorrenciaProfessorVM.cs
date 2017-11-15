using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Interface;

namespace Api.Data.ViewModels {
    public class InstituicaoCursoOcorrenciaProfessorVM : SelectVM {
        public InstituicaoCursoOcorrenciaProfessorVM() {

        }

        public ProfessorVM Professor { get; set; }
        public CursoGradeMateriaVM Materia { get; set; }
        public InstituicaoCursoTurmaVM Turma { get; set; }
        public InstituicaoCursoPeriodoVM Periodo { get; set; }

        public List<InstituicaoCursoOcorrenciaProfessorPeriodoAulaVM> PeriodosAula { get; set; }


    }
}