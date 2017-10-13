using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Models.Interface;

namespace Domain.Models {
    public class InstituicaoCursoPeriodo : IBaseModel {

        public InstituicaoCursoPeriodo () {

        }

        [Key]
        public long ID { get; set; }
        public InstituicaoCurso InstituicaoCurso { get; set; }
        public string Inicio { get; set; }
        public string Fim { get; set; }
        public bool Seg {get;set;}
        public bool Ter {get;set;}
        public bool Qua {get;set;}
        public bool Qui {get;set;}
        public bool Sex {get;set;}
        public bool Sab {get;set;}
        public bool Dom {get;set;}
        public bool Ativo { get; set; } = true;

    }
}