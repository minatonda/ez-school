using System;
using System.ComponentModel.DataAnnotations;
using Domain.Models.Interface;

namespace Domain.Models {
    public class Professor : IBaseModel {
        public Professor (Usuario usuario) {
            this.ID = usuario.ID;
            this.Usuario = usuario;
        }

        [Key]
        public string ID { get; set; }
        public Usuario Usuario { get; set; }
        public bool Ativo { get; set; } = true;

    }
}