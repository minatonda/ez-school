using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Interface;

namespace Api.Data.ViewModels {
    public class InstituicaoCategoriaVM : ShortVM {
        public long ID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

    }
}