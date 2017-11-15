using System;
using System.ComponentModel.DataAnnotations;
using Domain.Models.Interface;

namespace Domain.Models {
    public class InstituicaoCursoOcorrenciaNota : IBaseModel {
        public InstituicaoCursoOcorrenciaNota() {

        }

        [Key]
        public string ID { get; set; }
        public string Descricao { get; set; }
        public int Valor { get; set; }
        public DateTime DataLancamento { get; set; }
        public InstituicaoCursoOcorrenciaAluno InstituicaoCursoOcorrenciaAluno { get; set; }
        public InstituicaoCursoOcorrenciaPeriodoProfessor InstituicaoCursoOcorrenciaPeriodoProfessor { get; set; }

        public DateTime? Ativo { get; set; } = null;

    }
}