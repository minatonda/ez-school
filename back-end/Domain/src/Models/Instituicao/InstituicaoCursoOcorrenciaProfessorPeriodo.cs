using System;
using System.ComponentModel.DataAnnotations;
using Domain.Models.Interface;

namespace Domain.Models {
    public class InstituicaoCursoOcorrenciaProfessorPeriodoAula : IBaseModel {

        public InstituicaoCursoOcorrenciaProfessorPeriodoAula() {

        }

        [Key]
        public long ID { get; set; }
        public InstituicaoCursoOcorrenciaProfessor InstituicaoCursoOcorrenciaProfessor { get; set; }
        public string Inicio { get; set; }
        public string Fim { get; set; }
        public DayOfWeek Dia { get; set; }
        public DateTime? Ativo { get; set; } = null;

    }
}