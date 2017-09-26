using System;
using System.ComponentModel.DataAnnotations;
using Domain.Models.Interface;

namespace Domain.Models {
    public class InstituicaoAluno : IBaseModel {
        public InstituicaoAluno (Instituicao instituicao, Aluno aluno) {
            this.Instituicao = instituicao;
            this.Aluno = aluno;
        }

        [Key]
        public string ID { get; set; }
        public Instituicao Instituicao { get; set; }
        public Aluno Aluno { get; set; }
        public bool Ativo { get; set; } = true;

        public DateTime DataInicio { get; set; }
        public DateTime? DataExpiracao { get; set; }

    }
}