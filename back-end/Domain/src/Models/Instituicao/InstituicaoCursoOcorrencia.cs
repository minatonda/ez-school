using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Models.Interface;

namespace Domain.Models {
    public class InstituicaoCursoOcorrencia : IBaseModel {

        public InstituicaoCursoOcorrencia () {

        }

        [Key]
        public long ID { get; set; }
        public InstituicaoCurso InstituicaoCurso { get; set; }
        public bool Ativo { get; set; } = true;
        public Professor Coordenador { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataExpiracao { get; set; }

    }
}