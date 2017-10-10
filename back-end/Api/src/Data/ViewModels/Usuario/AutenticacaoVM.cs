using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Interface;

namespace Api.Data.ViewModels {
    public class AutenticacaoVM : SelectVM {

        public string access_token { get; set; }
        public DateTime created { get; set; }
        public DateTime expires { get; set; }
        public string time_zone { get; set; }

    }
}