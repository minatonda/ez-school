using System;
using System.ComponentModel.DataAnnotations;
using Domain.Models.Interface;

namespace Domain.Models {
    public class InstituicaoCursoOcorrenciaAluno : IBaseModel {

        [Key]
        public string ID { get; set; }
        public InstituicaoCursoOcorrencia InstituicaoCursoOcorrencia { get; set; }
        public Aluno Aluno { get; set; }
        public InstituicaoCursoPeriodo Periodo { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataExpiracao { get; set; }
        public ExpiracaoMotivo ExpiracaoMotivo { get; set; }

        public bool Ativo { get; set; } = true;

    }

    public enum ExpiracaoMotivo {
        CANCELADO = 1,
            TRANCADO = 2,
            TRANSFERIDO = 3
    }
}