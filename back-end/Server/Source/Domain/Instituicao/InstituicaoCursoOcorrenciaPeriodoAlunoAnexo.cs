using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.AnexoDomain;
using Domain.Common;

namespace Domain.InstituicaoDomain {

    public class InstituicaoCursoOcorrenciaPeriodoAlunoAnexo : IBaseModel {

        public InstituicaoCursoOcorrenciaPeriodoAlunoAnexo() {

        }

        [Key]
        public long ID { get; set; }
        public InstituicaoCursoOcorrenciaPeriodoAluno InstituicaoCursoOcorrenciaPeriodoAluno { get; set; }
        public InstituicaoCursoOcorrenciaPeriodoProfessor InstituicaoCursoOcorrenciaPeriodoProfessor { get; set; }
        public Anexo Anexo { get; set; }
        public DateTime? Ativo { get; set; } = null;

    }

}