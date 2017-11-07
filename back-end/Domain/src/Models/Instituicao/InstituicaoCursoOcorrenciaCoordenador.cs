using System;
using System.ComponentModel.DataAnnotations;
using Domain.Models.Interface;

namespace Domain.Models {
    public class InstituicaoCursoOcorrenciaCoordenador : IBaseModel {

        public InstituicaoCursoOcorrenciaCoordenador () {

        }

        [Key]
        public long ID { get; set; }
        public InstituicaoCursoOcorrencia InstituicaoCursoOcorrencia { get; set; }
        public Professor Professor { get; set; }
        public bool Confirmado  { get; set ;} = false;

        public DateTime? DataInicio { get; set; }
        public DateTime? DataExpiracao { get; set; }

        public DateTime? Ativo { get; set; } = null;

    }
}