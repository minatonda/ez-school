using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web;
using Api.InstituicaoApi;
using Api.UsuarioApi;
using Domain.CursoDomain;
using Domain.InstituicaoDomain;
using Domain.UsuarioDomain;
using Microsoft.AspNetCore.Mvc;

namespace Api.Common.Base {
    public class BaseController : Controller {

        public string _title = "";

        private UsuarioService usuarioService;
        private InstituicaoService instituicaoService;

        public BaseController(UsuarioRepository usuarioRepository, AreaInteresseRepository areaInteresseRepository, InstituicaoRepository instituicaoRepository, CursoRepository cursoRepository) {
            this.usuarioService = new UsuarioService(usuarioRepository, areaInteresseRepository);
            this.instituicaoService = new InstituicaoService(instituicaoRepository, cursoRepository);
        }

        public UsuarioInfoVM GetUsuarioInfoAuthenticated() {
            var usuarioId = this.User.FindFirst(x => x.Type == "usuario-info-id").Value;
            return this.usuarioService.Detail(usuarioId).UsuarioInfo;
        }

        public List<InstituicaoCursoOcorrenciaPeriodoProfessorVM> GetInstituicaoCursoOcorrenciaPeriodoProfessorOfAuthenticated() {
            return this.instituicaoService.AllInstituicaoCursoOcorrenciaPeriodoProfessorByProfessor(this.GetUsuarioInfoAuthenticated().ID);
        }

        public List<InstituicaoCursoOcorrenciaPeriodoAlunoVM> GetInstituicaoCursoOcorrenciaPeriodoAlunoOfAuthenticated() {
            return this.instituicaoService.AllInstituicaoCursoOcorrenciaPeriodoAlunoByAluno(this.GetUsuarioInfoAuthenticated().ID);
        }

        public List<string> GetAllRolesFromUsuario() {
            var roles = this.instituicaoService.AllInstituicaoColaboradorPerfilByUsuario(this.GetUsuarioInfoAuthenticated().ID)
            .SelectMany(x => x.Roles).AsEnumerable();
            roles = roles.Concat(this.GetUsuarioInfoAuthenticated().Roles.ToList());
            return roles.GroupBy(x => x).Select(x => x.First()).ToList();
        }

        public List<InstituicaoVM> GetAllInstituicaoFromUsuario() {
            return this.instituicaoService.AllInstituicaoByUsuario(this.GetUsuarioInfoAuthenticated().ID);
        }

        public bool IsAuthorized(BaseRole role) {
            if (this.GetUsuarioInfoAuthenticated().Roles.Contains(((int)BaseRole.ADMIN).ToString())) {
                return true;
            } else {
                return this.GetUsuarioInfoAuthenticated().Roles.Contains(((int)role).ToString());
            }
        }

        public bool IsAuthorizedInstituicao(long idInstituicao, BaseRole role) {
            if (this.IsAuthorized(role)) {
                return true;
            } else {
                return this.instituicaoService
                .AllInstituicaoColaboradorPerfilByUsuario(this.GetUsuarioInfoAuthenticated().ID, idInstituicao)
                .Any(x => x.Roles.Contains(((int)role).ToString()));
            }
        }

    }
}