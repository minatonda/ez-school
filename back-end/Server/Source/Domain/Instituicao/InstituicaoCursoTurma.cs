using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Common;

namespace Domain.InstituicaoDomain {
    
    public class InstituicaoCursoTurma : IBaseModel {

        [Key]
        public long ID { get; set; }
        public InstituicaoCurso InstituicaoCurso { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataExpiracao { get; set; }
        public DateTime? Ativo { get; set; } = null;

    }
}