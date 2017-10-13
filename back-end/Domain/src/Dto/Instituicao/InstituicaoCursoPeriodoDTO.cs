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
    public class InstituicaoCursoPeriodoDto
    {

        public InstituicaoCursoPeriodoDto()
        {

        }

        public InstituicaoCursoPeriodoDto(InstituicaoCursoPeriodo instituicaoCursoPeriodo)
        {
            this.ID = instituicaoCursoPeriodo.ID;
            this.Inicio = instituicaoCursoPeriodo.Inicio;
            this.Fim = instituicaoCursoPeriodo.Fim;
            this.Seg = instituicaoCursoPeriodo.Seg;
            this.Ter = instituicaoCursoPeriodo.Ter;
            this.Qua = instituicaoCursoPeriodo.Qua;
            this.Qui = instituicaoCursoPeriodo.Qui;
            this.Sex = instituicaoCursoPeriodo.Sex;
            this.Sab = instituicaoCursoPeriodo.Sab;
            this.Dom = instituicaoCursoPeriodo.Dom;
        }

        public long ID { get; set; }
        public string Inicio { get; set; }
        public string Fim { get; set; }
        public bool Seg {get;set;}
        public bool Ter {get;set;}
        public bool Qua {get;set;}
        public bool Qui {get;set;}
        public bool Sex {get;set;}
        public bool Sab {get;set;}
        public bool Dom {get;set;}

    }
}