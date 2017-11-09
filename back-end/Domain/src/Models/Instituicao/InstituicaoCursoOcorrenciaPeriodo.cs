using System;
using System.ComponentModel.DataAnnotations;
using Domain.Models.Interface;

namespace Domain.Models {
    public class InstituicaoCursoOcorrenciaPeriodo : IBaseModel {

        public InstituicaoCursoOcorrenciaPeriodo() {

        }

        [Key]
        public long ID { get; set; }
        public InstituicaoCursoOcorrencia InstituicaoCursoOcorrencia { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataExpiracao { get; set; }
        public DateTime? Ativo { get; set; } = null;

    }
}