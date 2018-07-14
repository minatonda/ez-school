using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.InstituicaoDomain {

    [Table("ICONota")]
    public class InstituicaoCursoOcorrenciaNota : IBaseModel {
        public InstituicaoCursoOcorrenciaNota() {

        }

        [Key]
        public long ID { get; set; }
        public string IDTag { get; set; }
        public double Valor { get; set; }
        public DateTime DataLancamento { get; set; }
        public InstituicaoCursoOcorrenciaPeriodoAluno InstituicaoCursoOcorrenciaPeriodoAluno { get; set; }
        public InstituicaoCursoOcorrenciaPeriodoProfessor InstituicaoCursoOcorrenciaPeriodoProfessor { get; set; }
        public DateTime? Ativo { get; set; } = null;

    }
}