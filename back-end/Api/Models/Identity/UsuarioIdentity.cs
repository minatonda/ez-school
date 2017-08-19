using Microsoft.AspNetCore.Identity;

namespace Api.Models.Identity {
    public class User : IdentityUser {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}