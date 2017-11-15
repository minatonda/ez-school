using System;
using System.ComponentModel.DataAnnotations;
using Domain.Models.Interface;

namespace Domain.Models {
    public class InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula : IBaseModel {

        public InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula() {

        }

        [Key]
        public long ID { get; set; }
        public InstituicaoCursoOcorrenciaPeriodoProfessor InstituicaoCursoOcorrenciaPeriodoProfessor { get; set; }
        public DayOfWeek Dia { get; set; }
        public string Inicio { get; set; }
        public string Fim { get; set; }
        public DateTime? Ativo { get; set; } = null;

    }
}