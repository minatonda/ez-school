using System;
using System.ComponentModel.DataAnnotations;
using Domain.Models.Interface;

namespace Domain.Models {
    public class InstituicaoCursoOcorrenciaAluno : IBaseModel {

        [Key]
        public long ID { get; set; }
        public InstituicaoCursoOcorrencia InstituicaoCursoOcorrencia { get; set; }
        public ExpiracaoMotivo ExpiracaoMotivo { get; set; }
        public Aluno Aluno { get; set; }
        public bool Confirmado { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataExpiracao { get; set; }
        public DateTime? Ativo { get; set; } = null;

    }

    public enum ExpiracaoMotivo {
        CANCELADO = 1,
        TRANCADO = 2,
        TRANSFERIDO = 3
    }
}