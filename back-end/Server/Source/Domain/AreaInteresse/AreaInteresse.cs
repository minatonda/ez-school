using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.CategoriaProfissionalDomain;
using Domain.UsuarioDomain;

namespace Domain.AreaInteresseDomain {

    public class AreaInteresse {

        public AreaInteresse() {

        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public UsuarioInfo UsuarioInfo { get; set; }
        public CategoriaProfissional CategoriaProfissional { get; set; }
        public string Descricao { get; set; }
        public DateTime? Ativo { get; set; } = null;
    }
}