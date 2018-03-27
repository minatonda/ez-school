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
            var usuarioId = this.User.FindFirst(x => x.Type == "IdUsuarioInfo").Value;
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

        public void Authorize(string role) {
            if (!this.GetUsuarioInfoAuthenticated().Roles.Contains(BaseRole.ADMIN) && !this.GetUsuarioInfoAuthenticated().Roles.Contains(role)) {
                throw new BaseException() {
                    HttpStatusCode = HttpStatusCode.Unauthorized,
                    Code = BaseExceptionCode.UNAUTHORIZED,
                    Infos = new List<BaseExceptionFieldInfo>(){
                        new BaseExceptionFieldInfo(){
                            Code=BaseExceptionCode.UNAUTHORIZED,
                            Field=BaseExceptionField.BASE_ROLE,
                            Value=role
                        }
                    }
                };
            }
        }

        public void IsAuthorizedInstituicao(long idInstituicao, string role) {
            var isAuthorizedInstituicao = this.instituicaoService
                .AllInstituicaoColaboradorPerfilByUsuario(this.GetUsuarioInfoAuthenticated().ID, idInstituicao)
                .Any(x => x.Roles.Contains(role));
            if (!isAuthorizedInstituicao) {
                try {
                    this.Authorize(BaseRole.ADMIN);
                } catch (BaseException) {
                    throw new BaseException() {
                        HttpStatusCode = HttpStatusCode.Unauthorized,
                        Code = BaseExceptionCode.UNAUTHORIZED,
                        Infos = new List<BaseExceptionFieldInfo>(){
                            new BaseExceptionFieldInfo(){
                                Code=BaseExceptionCode.UNAUTHORIZED,
                                Field=BaseExceptionField.INSTITUICAO_ROLE,
                                Value=role
                            }
                        }
                    };
                }
            }
        }

    }
}