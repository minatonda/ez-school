using System.Collections.Generic;
using System.Linq;
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
            this.Authorize(BaseRole.ADD_CURSO);
            this._cursoService.ValidateCurso(viewModel);
            this._cursoService.Add(viewModel);
        }

        [HttpPost("update")]
        public void Update([FromBody] CursoVM viewModel) {
            this.Authorize(BaseRole.EDIT_CURSO);
            this._cursoService.ValidateCurso(viewModel);
            this._cursoService.Update(viewModel);
        }

        [HttpDelete("disable")]
        public void Disable([FromQuery] long id) {
            this.Authorize(BaseRole.DISABLE_CURSO);
            this._cursoService.Disable(id);
        }

        [HttpGet("detail/{id}")]
        public CursoVM Detail(long id) {
            return this._cursoService.Detail(id);
        }

        [HttpGet]
        public List<CursoVM> All() {
            return this._cursoService.All();
        }

        [HttpGet("detail/{id}/curso-grade")]
        public List<CursoGradeVM> AllCursoGrade(long id) {
            return this._cursoService.Detail(id).Grades;
        }

        [HttpGet("detail/{id}/curso-grade/by-instituicao/{idInstituicao}")]
        public List<CursoGradeVM> AllCursoGradeByInstituicao(long id, long idInstituicao) {
            return this._cursoService.Detail(id).Grades.Where(x => x.Instituicao != null && x.Instituicao.ID == idInstituicao).ToList();
        }

        [HttpGet("detail/{id}/grade/detail/{idCursoGrade}/curso-grade-materia")]
        public List<CursoGradeMateriaVM> AllCursoGradeMateria(long id, long idCursoGrade) {
            return this._cursoService.Detail(id).Grades.Find(x => x.ID == idCursoGrade).Materias;
        }

    }
}