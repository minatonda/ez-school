using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models;
using Domain.Models.Interface;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Dto
{
    public class InstituicaoCursoOcorrenciaMateriaAusenciaDto
    {

        public InstituicaoCursoOcorrenciaMateriaAusenciaDto()
        {

        }

        public long ID { get; set; }
        public InstituicaoCursoOcorrenciaMateriaDto InstituicaoCursoOcorrenciaMateria { get; set; }
        public Aluno Aluno { get; set; }
        public Professor Professor { get; set; }
        public DateTime Data { get; set; }

    }
}