using System;
using System.ComponentModel.DataAnnotations;
using Domain.AnexoDomain;
using Domain.Common;
using Domain.CursoDomain;
using Domain.UsuarioDomain;

namespace Domain.InstituicaoDomain {

    public class InstituicaoCursoOcorrenciaPeriodoProfessorAnexo : IBaseModel {

        public InstituicaoCursoOcorrenciaPeriodoProfessorAnexo() {

        }

        [Key]
        public long ID { get; set; }
        public InstituicaoCursoOcorrenciaPeriodoProfessor InstituicaoCursoOcorrenciaPeriodoProfessor { get; set; }
        public Anexo Anexo { get; set; }
        public DateTime? Ativo { get; set; } = null;

    }
}