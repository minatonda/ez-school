using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.UsuarioDomain;

namespace Domain.InstituicaoDomain {

    [Table("ICOcorrencia")]
    public class InstituicaoCursoOcorrencia : IBaseModel {

        public InstituicaoCursoOcorrencia () {

        }

        [Key]
        public long ID { get; set; }
        public InstituicaoCurso InstituicaoCurso { get; set; }
        public DateTime? Ativo { get; set; } = null;
        public UsuarioInfo Coordenador { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataExpiracao { get; set; }

    }
}