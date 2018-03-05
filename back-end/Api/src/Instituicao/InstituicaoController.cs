using System.Collections.Generic;
using Api.Common.Base;
using Api.CursoApi;
using Domain.CursoDomain;
using Domain.InstituicaoDomain;
using Domain.UsuarioDomain;
using Microsoft.AspNetCore.Mvc;

namespace Api.InstituicaoApi {

    [Route("api/instituicao")]
    public class InstituicaoController : BaseController {

        private InstituicaoService _instituicaoService;

        public InstituicaoController(InstituicaoRepository instituicaoRepository, CursoRepository cursoRepository, UsuarioRepository usuarioRepository, AreaInteresseRepository areaInteresseRepository) : base(usuarioRepository, areaInteresseRepository) {
            this._instituicaoService = new InstituicaoService(instituicaoRepository, cursoRepository);
        }

        [HttpPut("add")]
        public void Add([FromBody] InstituicaoVM viewModel) {
            this._instituicaoService.Add(viewModel);
        }

        [HttpPut("{id}/instituicao-curso/add")]
        public void AddInstituicaoCurso(long id, [FromBody] InstituicaoCursoVM viewModel) {
            this._instituicaoService.AddInstituicaoCurso(id, viewModel);
        }

        [HttpPut("{id}/instituicao-curso/{idInstituicaoCurso}/instituicao-curso-ocorrencia/add")]
        public void AddInstituicaoCursoOcorrencia(long id, long idInstituicaoCurso, [FromBody] InstituicaoCursoOcorrenciaVM viewModel) {
            this._instituicaoService.AddInstituicaoCursoOcorrencia(idInstituicaoCurso, viewModel);
        }

        [HttpPost("update")]
        public void Update([FromBody] InstituicaoVM viewModel) {
            this._instituicaoService.Update(viewModel);
        }

        [HttpPost("{id}/instituicao-curso/update")]
        public void UpdateCurso(long id, [FromBody] InstituicaoCursoVM viewModel) {
            this._instituicaoService.UpdateInstituicaoCurso(id, viewModel);
        }

        [HttpPost("{id}/instituicao-curso/{idInstituicaoCurso}/instituicao-curso-ocorrencia/update")]
        public void UpdateInstituicaoCursoOcorrencia(long id, long idInstituicaoCurso, [FromBody] InstituicaoCursoOcorrenciaVM viewModel) {
            this._instituicaoService.UpdateInstituicaoCursoOcorrencia(idInstituicaoCurso, viewModel);
        }

        [HttpDelete("disable")]
        public void Disable([FromQuery] long id) {
            this._instituicaoService.Disable(id);
        }

        [HttpDelete("{id}/instituicao-curso/disable")]
        public void DisableInstituicaoCurso(long id, [FromQuery]long idInstituicaoCurso) {
            this._instituicaoService.DisableInstituicaoCurso(idInstituicaoCurso);
        }

        [HttpDelete("{id}/instituicao-curso/{idInstituicaoCurso}/instituicao-curso-ocorrencia/disable")]
        public void DisableInstituicaoCursoOcorrencia(long id, long idInstituicaoCurso, [FromQuery]long idInstituicaoCursoOcorrencia) {
            this._instituicaoService.DisableInstituicaoCursoOcorrencia(idInstituicaoCursoOcorrencia);
        }

        [HttpGet("{id}")]
        public InstituicaoVM Detail(long id) {
            return this._instituicaoService.Detail(id);
        }

        [HttpGet("{id}/instituicao-curso/{idInstituicaoCurso}")]
        public InstituicaoCursoVM DetailInstituicaoCurso(long id, long idInstituicaoCurso) {
            return this._instituicaoService.DetailInstituicaoCurso(idInstituicaoCurso);
        }

        [HttpGet("{id}/instituicao-curso/{idInstituicaoCurso}/instituicao-curso-ocorrencia/{idInstituicaoCursoOcorrencia}")]
        public InstituicaoCursoOcorrenciaVM DetailInstituicaoCursoOcorrencia(long id, long idInstituicaoCurso, long idInstituicaoCursoOcorrencia) {
            return this._instituicaoService.DetailInstituicaoCursoOcorrencia(idInstituicaoCursoOcorrencia);
        }

        [HttpGet]
        public List<InstituicaoVM> All() {
            return this._instituicaoService.All();
        }

        [HttpGet("{id}/instituicao-curso/{idInstituicaoCurso}/instituicao-curso-periodo")]
        public List<InstituicaoCursoPeriodoVM> AllInstituicaoCursoPeriodo(long id, long idInstituicaoCurso) {
            return this._instituicaoService.AllInstituicaoCursoPeriodo(idInstituicaoCurso);
        }

        [HttpGet("{id}/instituicao-curso/{idInstituicaoCurso}/instituicao-curso-turma")]
        public List<InstituicaoCursoTurmaVM> AllInstituicaoCursoTurma(long id, long idInstituicaoCurso) {
            return this._instituicaoService.AllInstituicaoCursoTurma(idInstituicaoCurso);
        }

        [HttpGet("{id}/instituicao-curso/{idInstituicaoCurso}/curso-grade-materia")]
        public List<CursoGradeMateriaVM> AllCursoGradeMateria(long id, long idInstituicaoCurso) {
            return this._instituicaoService.AllCursoGradeMateria(idInstituicaoCurso);
        }

        [HttpGet("{id}/instituicao-curso")]
        public List<InstituicaoCursoVM> AllInstituicaoCurso(long id) {
            return this._instituicaoService.AllInstituicaoCurso(id);
        }

        [HttpGet("{id}/instituicao-curso/{idInstituicaoCurso}/instituicao-curso-ocorrencia")]
        public List<InstituicaoCursoOcorrenciaVM> AllInstituicaoCursoOcorrencia(long id, long idInstituicaoCurso) {
            return this._instituicaoService.AllInstituicaoCursoOcorrencia(idInstituicaoCurso);
        }

    }
}