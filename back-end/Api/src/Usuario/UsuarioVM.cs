using Api.Common.Base;

namespace Api.UsuarioApi {
    public class UsuarioVM : BaseVM {
        
        public string Username { get; set; }
        public string Password { get; set; }
        public UsuarioInfoVM UsuarioInfo { get; set; }

    }
}