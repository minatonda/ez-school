using System.Collections.Generic;
using Api.Common.Base;
using Domain.CursoDomain;
using Domain.UsuarioDomain;
using Microsoft.AspNetCore.Mvc;

namespace Api.CursoApi {

    [Route("api/curso")]
    public class CursoController : BaseController {

        private CursoService _cursoService;

        public CursoController(CursoRepository cursoRepository, UsuarioRepository usuarioRepository) : base(usuarioRepository) {
            this._cursoService = new CursoService(cursoRepository);
        }

        [HttpPut("add")]
        public void Add([FromBody] CursoVM viewModel) {
            this._cursoService.Add(viewModel);
        }

        [HttpPost("update")]
        public void Update([FromBody] CursoVM viewModel) {
            this._cursoService.Update(viewModel);
        }

        [HttpDelete("disable")]
        public void Disable([FromQuery] long id) {
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
            return this._cursoService.Detail(id).Grades.Find(x => x.ID == idCursoGrade.ToString()).Materias;
        }

    }
}