using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Interface;

namespace Api.Data.ViewModels
{
    public class InstituicaoCursoPeriodoVM : SelectVM
    {
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