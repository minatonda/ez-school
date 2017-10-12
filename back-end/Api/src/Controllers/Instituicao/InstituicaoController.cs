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
        public List<InstituicaoVM> Get()
        {
            return this._instituicaoService.GetAll();
        }
        [HttpGet("{id}")]
        public InstituicaoVM GetDetail(long id)
        {
            return this._instituicaoService.GetDetail(id);
        }
        [HttpPut("add")]
        public InstituicaoVM Put([FromBody] InstituicaoVM viewModel)
        {
            return this._instituicaoService.Add(viewModel);
        }
        [HttpPost("upd")]
        public InstituicaoVM Post([FromBody] InstituicaoVM viewModel)
        {
            return this._instituicaoService.Update(viewModel);
        }
        [HttpDelete("del")]
        public void Delete([FromQuery]long id)
        {
            this._instituicaoService.Delete(id);
        }

        [HttpGet("{id}/categorias")]
        public List<InstituicaoCategoriaVM> GetCategorias(long id)
        {
            return this._instituicaoService.GetCategorias(id);
        }
        [HttpGet("{id}/categorias/add")]
        public void AddCategorias(long id, [FromBody] InstituicaoCategoriaVM viewModel)
        {
            this._instituicaoService.AddCategoria(id, viewModel);
        }
        [HttpGet("{id}/categorias/del")]
        public void DeleteCategorias(long id, [FromQuery] long idCategoria)
        {
            this._instituicaoService.DeleteCategoria(id, idCategoria);
        }

        [HttpPut("{id}/cursos/add")]
        public void AddCurso(long id, [FromBody] InstituicaoCursoVM viewModel)
        {
            this._instituicaoService.AddCurso(id, viewModel);
        }
        [HttpPost("{id}/cursos/renew")]
        public void RenewCurso(long id, [FromBody] InstituicaoCursoVM viewModel)
        {
            this._instituicaoService.RenewCurso(id, viewModel);
        }
        [HttpDelete("{id}/cursos/del")]
        public void DeleteCurso(long id, [FromQuery] long idCategoria)
        {
            this._instituicaoService.DisableCurso(id, idCategoria);
        }
        [HttpGet("{id}/cursos/{idCurso}")]
        public InstituicaoCursoVM GetCurso(long id, long idCurso)
        {
            return this._instituicaoService.GetCurso(id, idCurso);
        }
        [HttpGet("{id}/cursos")]
        public List<InstituicaoCursoVM> GetCursos(long id)
        {
            return this._instituicaoService.GetCursos(id);
        }

        [HttpGet("{id}/cursos/{idCurso}/ocorrencias")]
        public List<InstituicaoCursoOcorrenciaVM> GetCursoOcorrencias(long id, long idCurso)
        {
            return this._instituicaoService.GetCursoOcorrencias(id, idCurso);
        }
        [HttpGet("{id}/cursos/{idCurso}/ocorrencias/{idOcorrencia}")]
        public InstituicaoCursoOcorrenciaVM GetCursoOcorrencia(long id, long idCurso, long idOcorrencia)
        {
            return this._instituicaoService.GetCursoOcorrencia(id, idCurso, idOcorrencia);
        }
        [HttpGet("{id}/cursos/{idCurso}/ocorrencias/add")]
        public void AddCursoOcorrencia(long id, long idCurso, [FromBody] InstituicaoCursoOcorrenciaVM viewModel)
        {
            this._instituicaoService.AddCursoOcorrencia(id, idCurso, viewModel);
        }
        [HttpGet("{id}/cursos/{idCurso}/ocorrencias/del")]
        public void DeleteCursoOcorrencia(long id, long idCurso, [FromQuery] long idOcorrencia)
        {
            this._instituicaoService.DeleteCursoOcorrencia(id, idCurso, idOcorrencia);
        }
    }
}