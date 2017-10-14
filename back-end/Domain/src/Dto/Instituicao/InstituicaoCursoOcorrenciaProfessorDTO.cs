using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models;
using Domain.Models.Interface;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Dto {
    public class InstituicaoCursoOcorrenciaProfessorDto {

        public InstituicaoCursoOcorrenciaProfessorDto(InstituicaoCursoOcorrenciaMateria instituicaoCursoOcorrenciaMateria, InstituicaoCursoOcorrenciaMateriaProfessor instituicaoCursoOcorrenciaMateriaProfessor) {
            this.Materia = instituicaoCursoOcorrenciaMateria.Materia;
            this.Professor = instituicaoCursoOcorrenciaMateriaProfessor.Professor;
        }

        public Materia Materia { get; set; }
        public Professor Professor { get; set; }

    }
}