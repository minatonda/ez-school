using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Service;
using Api.Data.ViewModels;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers {
    [Route("api/instituicao")]
    public class InstituicaoController : Controller {

        private InstituicaoService _instituicaoService;
        public InstituicaoController(InstituicaoRepository instituicaoRepository, CursoRepository cursoRepository) {
            this._instituicaoService = new InstituicaoService(instituicaoRepository, cursoRepository);
        }

        [HttpPut("add")]
        public InstituicaoVM Add([FromBody] InstituicaoVM viewModel) {
            return this._instituicaoService.Add(viewModel);
        }

        [HttpPost("update")]
        public InstituicaoVM Update([FromBody] InstituicaoVM viewModel) {
            return this._instituicaoService.Update(viewModel);
        }

        [HttpDelete("disable")]
        public void Disable([FromQuery] long id) {
            this._instituicaoService.Disable(id);
        }

        [HttpPut("{id}/curso/add")]
        public void AddCurso(long id, [FromBody] InstituicaoCursoVM viewModel) {
            this._instituicaoService.AddCurso(id, viewModel);
        }

        [HttpPost("{id}/curso/renew")]
        public void UpdateCurso(long id, [FromBody] InstituicaoCursoVM viewModel) {
            this._instituicaoService.UpdateCurso(id, viewModel);
        }

        [HttpGet("{id}")]
        public InstituicaoVM Detail(long id) {
            return this._instituicaoService.Detail(id);
        }

        [HttpGet]
        public List<InstituicaoVM> All() {
            return this._instituicaoService.All();
        }

        [HttpDelete("{id}/cursos/{idCurso}/disable")]
        public void DisableCurso(long id, long idCurso) {
            this._instituicaoService.DisableCurso(id, idCurso);
        }

        [HttpGet("{id}/curso/{idCurso}")]
        public InstituicaoCursoVM DetailCurso(long id, long idCurso) {
            return this._instituicaoService.DetailCurso(id, idCurso);
        }

        [HttpGet("{id}/curso/{idCurso}/periodo")]
        public List<InstituicaoCursoPeriodoVM> AllPeriodo(long id, long idCurso) {
            return this._instituicaoService.AllPeriodo(id, idCurso);
        }

        [HttpGet("{id}/curso/{idCurso}/periodo/{idPeriodo}/periodo-aula-disponivel")]
        public List<InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaVM> AllPeriodoAulaDisponivel(long id, long idCurso, long idPeriodo) {
            return this._instituicaoService.AllPeriodoAulaDisponivel(id, idCurso, idPeriodo);
        }

        [HttpGet("{id}/curso/{idCurso}/turma")]
        public List<InstituicaoCursoTurmaVM> AllTurma(long id, long idCurso) {
            return this._instituicaoService.AllTurma(id, idCurso);
        }

        [HttpGet("{id}/curso/{idCurso}/grade-materias")]
        public List<CursoGradeMateriaVM> AllGradeMateria(long id, long idCurso) {
            return this._instituicaoService.AllCursoGradeMateria(id, idCurso);
        }

        [HttpGet("{id}/curso")]
        public List<InstituicaoCursoVM> AllCurso(long id) {
            return this._instituicaoService.AllCurso(id);
        }

        [HttpGet("{id}/curso/{idCurso}/ocorrencia")]
        public List<InstituicaoCursoOcorrenciaVM> AllCursoOcorrencia(long id, long idCurso) {
            return this._instituicaoService.AllCursoOcorrencia(id, idCurso);
        }

        [HttpGet("{id}/curso/{idCurso}/ocorrencia/{dataInicio}")]
        public InstituicaoCursoOcorrenciaVM DetailCursoOcorrencia(long id, long idCurso, string dataInicio) {
            return this._instituicaoService.DetailCursoOcorrencia(id, idCurso, dataInicio);
        }

        [HttpPut("{id}/curso/{idCurso}/ocorrencia/add")]
        public void AddCursoOcorrencia(long id, long idCurso, [FromBody] InstituicaoCursoOcorrenciaVM viewModel) {
            this._instituicaoService.AddCursoOcorrencia(id, idCurso, viewModel);
        }

    }
}