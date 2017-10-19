using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models;
using Domain.Models.Interface;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Dto {
    public class InstituicaoCursoTurmaDto {
        
        public InstituicaoCursoTurmaDto() { }
        public InstituicaoCursoTurmaDto(InstituicaoCursoTurma instituicaoCursoTurma) {
            this.ID = instituicaoCursoTurma.ID;
            this.Nome = instituicaoCursoTurma.Nome;
            this.Descricao = instituicaoCursoTurma.Descricao;
            this.DataInicio = instituicaoCursoTurma.DataInicio;
            this.DataExpiracao = instituicaoCursoTurma.DataExpiracao;
        }

        public long ID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataExpiracao { get; set; }

    }
}