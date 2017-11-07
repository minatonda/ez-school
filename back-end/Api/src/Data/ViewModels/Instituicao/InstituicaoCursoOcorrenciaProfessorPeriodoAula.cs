using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Interface;

namespace Api.Data.ViewModels {
    public class InstituicaoCursoOcorrenciaProfessorPeriodoAulaVM : SelectVM {
        public InstituicaoCursoOcorrenciaProfessorPeriodoAulaVM() {

        }

        public InstituicaoCursoOcorrenciaProfessorVM InstituicaoCursoOcorrenciaProfessor { get; set; }
        public string Inicio { get; set; }
        public string Fim { get; set; }
        public DayOfWeek Dia { get; set; }

    }
}