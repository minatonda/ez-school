using System;
using Api.Common.ViewModels;

namespace Api.UsuarioApi {

    public class AutenticacaoVM : SelectVM {

        public string access_token { get; set; }
        public DateTime created { get; set; }
        public DateTime expires { get; set; }
        public string time_zone { get; set; }

    }
}