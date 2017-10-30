using System;
using System.ComponentModel.DataAnnotations;
using Domain.Models.Interface;

namespace Domain.Models {
    public class InstituicaoCursoOcorrenciaMateriaNota : IBaseModel {
        public InstituicaoCursoOcorrenciaMateriaNota () {

        }

        [Key]
        public string ID { get; set; }
        public string Descricao { get; set; }
        public int Valor { get; set; }
        public DateTime DataLancamento { get; set; }
        public InstituicaoCursoOcorrenciaAluno InstituicaoCursoOcorrenciaAluno { get; set; }
        public InstituicaoCursoOcorrenciaMateria InstituicaoCursoOcorrenciaMateria { get; set; }
        
        public bool Ativo { get; set; } = true;

    }
}