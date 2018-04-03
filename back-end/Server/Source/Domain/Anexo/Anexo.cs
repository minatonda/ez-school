using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.AnexoDomain {

    public class Anexo : IBaseModel {

        public Anexo() {

        }

        [Key]
        public string ID { get; set; }
        public string Nome { get; set; }
        public DateTime? DataCarregamento { get; set; }
        public DateTime? Ativo { get; set; }

    }
}