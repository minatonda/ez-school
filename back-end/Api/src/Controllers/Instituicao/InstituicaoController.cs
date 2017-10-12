using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Service;
using Api.Data.ViewModels;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/instituicao")]
    public class InstituicaoController : Controller
    {

        private InstituicaoService _instituicaoService;
        public InstituicaoController(InstituicaoRepository instituicaoRepository, CursoRepository cursoRepository)
        {
            this._instituicaoService = new InstituicaoService(instituicaoRepository, cursoRepository);
        }
        [HttpGet]
        public List<InstituicaoVM> All()
        {
            return this._instituicaoService.All();
        }
        [HttpGet("{id}")]
        public InstituicaoVM Detail(long id)
        {
            return this._instituicaoService.Detail(id);
        }
        [HttpPut("add")]
        public InstituicaoVM Add([FromBody] InstituicaoVM viewModel)
        {
            return this._instituicaoService.Add(viewModel);
        }
        [HttpPost("update")]
        public InstituicaoVM Update([FromBody] InstituicaoVM viewModel)
        {
            return this._instituicaoService.Update(viewModel);
        }
        [HttpDelete("disable")]
        public void Disable([FromQuery]long id)
        {
            this._instituicaoService.Disable(id);
        }

        [HttpGet("{id}/categoria")]
        public List<InstituicaoCategoriaVM> AllCategoria(long id)
        {
            return this._instituicaoService.AllCategoria(id);
        }
        [HttpGet("{id}/categoria/add")]
        public void AddCategoria(long id, [FromBody] InstituicaoCategoriaVM viewModel)
        {
            this._instituicaoService.AddCategoria(id, viewModel);
        }
        [HttpDelete("{id}/categoria/del")]
        public void DeleteCategoria(long id, [FromQuery] long idCategoria)
        {
            this._instituicaoService.DeleteCategoria(id, idCategoria);
        }

        [HttpPut("{id}/curso/add")]
        public void AddCurso(long id, [FromBody] InstituicaoCursoVM viewModel)
        {
            this._instituicaoService.AddCurso(id, viewModel);
        }
        [HttpPost("{id}/curso/renew")]
        public void RenewCurso(long id, [FromBody] InstituicaoCursoVM viewModel)
        {
            this._instituicaoService.RenewCurso(id, viewModel);
        }
        [HttpDelete("{id}/cursos/disable")]
        public void DisableCurso(long id, [FromQuery] long idCategoria)
        {
            this._instituicaoService.DisableCurso(id, idCategoria);
        }
        [HttpGet("{id}/curso/{idCurso}")]
        public InstituicaoCursoVM DetailCurso(long id, long idCurso)
        {
            return this._instituicaoService.DetailCurso(id, idCurso);
        }
        [HttpGet("{id}/curso")]
        public List<InstituicaoCursoVM> AllCurso(long id)
        {
            return this._instituicaoService.AllCurso(id);
        }

        [HttpGet("{id}/curso/{idCurso}/ocorrencia")]
        public List<InstituicaoCursoOcorrenciaVM> AllCursoOcorrencia(long id, long idCurso)
        {
            return this._instituicaoService.AllCursoOcorrencia(id, idCurso);
        }
        [HttpGet("{id}/curso/{idCurso}/ocorrencia/{idOcorrencia}")]
        public InstituicaoCursoOcorrenciaVM DetailCursoOcorrencia(long id, long idCurso, long idOcorrencia)
        {
            return this._instituicaoService.DetailCursoOcorrencia(id, idCurso, idOcorrencia);
        }
        [HttpGet("{id}/curso/{idCurso}/ocorrencia/add")]
        public void AddCursoOcorrencia(long id, long idCurso, [FromBody] InstituicaoCursoOcorrenciaVM viewModel)
        {
            this._instituicaoService.AddCursoOcorrencia(id, idCurso, viewModel);
        }
        [HttpGet("{id}/curso/{idCurso}/ocorrencia/disable")]
        public void DisableCursoOcorrencia(long id, long idCurso, [FromQuery] long idOcorrencia)
        {
            this._instituicaoService.DeleteCursoOcorrencia(id, idCurso, idOcorrencia);
        }
    }
}