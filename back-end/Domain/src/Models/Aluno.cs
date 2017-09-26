using System;
using System.ComponentModel.DataAnnotations;
using Domain.Models.Interface;

namespace Domain.Models {
    public class Aluno : IBaseModel {
        public Aluno (Usuario usuario) {
            this.ID = usuario.ID;
            this.Usuario = usuario;
        }

        [Key]
        public string ID { get; set; }
        public Usuario Usuario { get; set; }
        public bool Ativo { get; set; } = true;

    }
}