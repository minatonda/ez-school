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
    public class InstituicaoCursoOcorrenciaAlunoDto
    {

        public InstituicaoCursoOcorrenciaAlunoDto()
        {

        }

        public long ID { get; set; }
        public Aluno Aluno { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataExpiracao { get; set; }

    }
}