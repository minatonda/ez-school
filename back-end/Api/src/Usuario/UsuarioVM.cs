using Api.Common.ViewModels;

namespace Api.UsuarioApi {
    public class UsuarioVM : SelectVM {
        
        public string Username { get; set; }
        public string Password { get; set; }
        public UsuarioInfoVM UsuarioInfo { get; set; }

    }
}