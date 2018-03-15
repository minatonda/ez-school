using System.Collections.Generic;
using Api.Common.Base;
using Api.InstituicaoApi;
using Domain.CursoDomain;
using Domain.InstituicaoDomain;
using Domain.UsuarioDomain;
using Microsoft.AspNetCore.Mvc;

namespace Api.CursoApi {

    [Route("api/curso")]
    public class CursoController : BaseController {

        private CursoService _cursoService;

        public CursoController(CursoRepository cursoRepository, UsuarioRepository usuarioRepository, AreaInteresseRepository areaInteresseRepository, InstituicaoRepository instituicaoRepository) : base(usuarioRepository, areaInteresseRepository, instituicaoRepository, cursoRepository) {
            this._cursoService = new CursoService(cursoRepository);
        }

        [HttpPut("add")]
        public void Add([FromBody] CursoVM viewModel) {
            if (!this.IsAuthorized(BaseRole.ADD_CURSO)) {
                throw new BaseUnauthorizedException(BaseRole.ADD_CURSO);
            }
            this._cursoService.Add(viewModel);
        }

        [HttpPost("update")]
        public void Update([FromBody] CursoVM viewModel) {
            if (!this.IsAuthorized(BaseRole.EDIT_CURSO)) {
                throw new BaseUnauthorizedException(BaseRole.EDIT_CURSO);
            }
            this._cursoService.Update(viewModel);
        }

        [HttpDelete("disable")]
        public void Disable([FromQuery] long id) {
            if (!this.IsAuthorized(BaseRole.DISABLE_CURSO)) {
                throw new BaseUnauthorizedException(BaseRole.DISABLE_CURSO);
            }
            this._cursoService.Disable(id);
        }

        [HttpGet("{id}")]
        public CursoVM Detail(long id) {
            return this._cursoService.Detail(id);
        }

        [HttpGet]
        public List<CursoVM> All() {
            return this._cursoService.All();
        }

        [HttpGet("{id}/curso-grade")]
        public List<CursoGradeVM> AllCursoGrade(long id) {
            return this._cursoService.Detail(id).Grades;
        }

        [HttpGet("{id}/grade/{idCursoGrade}/curso-grade-materia")]
        public List<CursoGradeMateriaVM> AllCursoGradeMateria(long id, long idCursoGrade) {
            return this._cursoService.Detail(id).Grades.Find(x => x.ID == idCursoGrade).Materias;
        }

    }
}