using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Interface;

namespace Api.Data.ViewModels {
    public class InstituicaoCursoVM : SelectVM {

        public InstituicaoCursoVM() {
            this.Periodos = new List<InstituicaoCursoPeriodoVM>();
            this.Turmas = new List<InstituicaoCursoTurmaVM>();
        }

        public CursoVM Curso { get; set; }
        public CursoGradeVM CursoGrade { get; set; }
        public List<InstituicaoCursoPeriodoVM> Periodos { get; set; }
        public List<InstituicaoCursoTurmaVM> Turmas { get; set; }

        public DateTime? DataInicio { get; set; }
        public DateTime? DataExpiracao { get; set; }

    }
}