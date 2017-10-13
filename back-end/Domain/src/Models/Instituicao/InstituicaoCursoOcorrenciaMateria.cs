using System;
using System.ComponentModel.DataAnnotations;
using Domain.Models.Interface;

namespace Domain.Models {
    public class InstituicaoCursoOcorrenciaMateria : IBaseModel {

        public InstituicaoCursoOcorrenciaMateria () {

        }

        [Key]
        public long ID { get; set; }
        public InstituicaoCursoOcorrencia InstituicaoCursoOcorrencia { get; set; }
        public Materia Materia { get; set; }
        public Professor Professor { get; set; }
        public InstituicaoCursoPeriodo Periodo { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataExpiracao { get; set; }

        public bool Ativo { get; set; } = true;

    }
}