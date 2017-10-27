using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Interface;

namespace Api.Data.ViewModels {
    public class InstituicaoCursoOcorrenciaVM : SelectVM {
        public InstituicaoCursoOcorrenciaVM() {
            this.Alunos = new List<InstituicaoCursoOcorrenciaAlunoVM>();
            this.Professores = new List<InstituicaoCursoOcorrenciaProfessorVM>();
        }

        public ProfessorVM Coordenador { get; set; }
        public List<InstituicaoCursoOcorrenciaAlunoVM> Alunos { get; set; }
        public List<InstituicaoCursoOcorrenciaProfessorVM> Professores { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataExpiracao { get; set; }

    }
}