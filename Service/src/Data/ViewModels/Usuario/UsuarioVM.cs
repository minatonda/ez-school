using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Interface;

namespace Service.Data.ViewModels {
    public class UsuarioVM : ShortVM {
        public string ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UsuarioInfoVM UsuarioInfo { get; set; }

    }
}