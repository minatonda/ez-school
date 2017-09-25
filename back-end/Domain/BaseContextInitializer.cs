using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain {
    public static class BaseContextInitializer {
        public static void Initialize (BaseContext context) {
            var listUsuario = getBaseUsuarios ();

            context.Usuarios.AddRange (listUsuario);
            context.SaveChanges ();
        }

        public static Usuario[] getBaseUsuarios () {
            var usuario = new Usuario () {
                Username = "dev",
                Password = "dev",
                Email = "dev@hiperfast.com"
            };
            var usuarioInfo = new UsuarioInfo () {
                ID = usuario.ID,
                Nome = "Matheus Carvalho",
                DataNascimento = DateTime.ParseExact ("1994-12-19", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture),
                CPF = "42187917835",
                RG = "421920816"
            };
            usuario.UsuarioInfo = usuarioInfo;
            return new Usuario[] {
                usuario
            };
        }

    }
}