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
    public class InstituicaoCursoOcorrenciaMateriaDto
    {

        public InstituicaoCursoOcorrenciaMateriaDto()
        {

        }

        public long ID { get; set; }
        public Materia Materia { get; set; }
        public Professor Professor { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataExpiracao { get; set; }
    }
}