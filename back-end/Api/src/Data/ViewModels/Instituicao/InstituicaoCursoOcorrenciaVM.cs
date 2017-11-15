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
            this.InstituicaoCursoOcorrenciaPeriodos = new List<InstituicaoCursoOcorrenciaPeriodoVM>();
        }

        public ProfessorVM Coordenador { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataExpiracao { get; set; }
        public List<InstituicaoCursoOcorrenciaPeriodoVM> InstituicaoCursoOcorrenciaPeriodos { get; set; }

    }
}