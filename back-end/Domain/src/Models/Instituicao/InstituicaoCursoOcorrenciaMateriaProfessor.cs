using System;
using System.ComponentModel.DataAnnotations;
using Domain.Models.Interface;

namespace Domain.Models {
    public class InstituicaoCursoOcorrenciaMateriaProfessor : IBaseModel {

        public InstituicaoCursoOcorrenciaMateriaProfessor () {

        }

        [Key]
        public long ID { get; set; }
        public InstituicaoCursoOcorrenciaMateria InstituicaoCursoOcorrenciaMateria { get; set; }
        public Professor Professor { get; set; }
        public InstituicaoCursoPeriodo Periodo { get; set; }
        public bool Confirmado {get;set;}

        public DateTime? DataInicio { get; set; }
        public DateTime? DataExpiracao { get; set; }

        public bool Ativo { get; set; } = true;

    }
}