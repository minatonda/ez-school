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
    public class InstituicaoCursoOcorrenciaDto
    {

        public InstituicaoCursoOcorrenciaDto()
        {

        }

        public long ID { get; set; }
        public Professor Coordenador { get; set; }
        public List<Aluno> Alunos { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataExpiracao { get; set; }

    }
}