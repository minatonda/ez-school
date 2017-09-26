using System;
using System.ComponentModel.DataAnnotations;
using Domain.Models.Interface;

namespace Domain.Models {
    public class InstituicaoProfessor : IBaseModel {
        public InstituicaoProfessor (Instituicao instituicao, Professor professor) {
            this.Instituicao = instituicao;
            this.Professor = professor;
        }

        [Key]
        public string ID { get; set; }
        public Instituicao Instituicao { get; set; }
        public Professor Professor { get; set; }
        public bool Ativo { get; set; } = true;

        public DateTime DataInicio { get; set; }
        public DateTime? DataExpiracao { get; set; }

    }
}