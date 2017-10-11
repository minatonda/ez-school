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

        private InstituicaoService _instituicaoRepository;
        public InstituicaoController(InstituicaoRepository instituicaoRepository, CursoRepository cursoRepository)
        {
            this._instituicaoRepository = new InstituicaoService(instituicaoRepository, cursoRepository);
        }
        [HttpGet]
        public List<InstituicaoVM> Get()
        {
            return this._instituicaoRepository.GetAll();
        }
        [HttpGet("{id}")]
        public InstituicaoVM GetDetail(long id)
        {
            return this._instituicaoRepository.GetDetail(id);
        }
        [HttpPut("add")]
        public InstituicaoVM Put([FromBody] InstituicaoVM viewModel)
        {
            return this._instituicaoRepository.Add(viewModel);
        }
        [HttpPost("upd")]
        public InstituicaoVM Post([FromBody] InstituicaoVM viewModel)
        {
            return this._instituicaoRepository.Update(viewModel);
        }
        [HttpDelete("del")]
        public void Delete([FromQuery]long id)
        {
            this._instituicaoRepository.Delete(id);
        }

        [HttpGet("{id}/categorias")]
        public List<InstituicaoCategoriaVM> GetCategorias(long id)
        {
            return this._instituicaoRepository.GetCategorias(id);
        }
        [HttpGet("{id}/categorias/add")]
        public void AddCategorias(long id, [FromBody] InstituicaoCategoriaVM viewModel)
        {
            this._instituicaoRepository.AddCategoria(id, viewModel);
        }
        [HttpGet("{id}/categorias/del")]
        public void DeleteCategorias(long id, [FromQuery] long idCategoria)
        {
            this._instituicaoRepository.DeleteCategoria(id, idCategoria);
        }

        [HttpPut("{id}/cursos/add")]
        public void AddCurso(long id, [FromBody] InstituicaoCursoVM viewModel)
        {
            this._instituicaoRepository.AddCurso(id, viewModel);
        }
        [HttpPost("{id}/cursos/renew")]
        public void RenewCurso(long id, [FromBody] InstituicaoCursoVM viewModel)
        {
            this._instituicaoRepository.RenewCurso(id, viewModel);
        }
        [HttpDelete("{id}/cursos/del")]
        public void DeleteCurso(long id, [FromQuery] long idCategoria)
        {
            this._instituicaoRepository.DisableCurso(id, idCategoria);
        }
        [HttpGet("{id}/cursos/{idCurso}")]
        public InstituicaoCursoVM GetCurso(long id, long idCurso)
        {
            return this._instituicaoRepository.GetCurso(id, idCurso);
        }
        [HttpGet("{id}/cursos")]
        public List<InstituicaoCursoVM> GetCursos(long id)
        {
            return this._instituicaoRepository.GetCursos(id);
        }

        [HttpGet("{id}/cursos/{idCurso}/ocorrencias")]
        public List<InstituicaoCursoOcorrenciaVM> GetCursoOcorrencias(long id, long idCurso)
        {
            return this._instituicaoRepository.GetCursoOcorrencias(id, idCurso);
        }
        [HttpGet("{id}/cursos/{idCurso}/ocorrencias/{idOcorrencia}")]
        public InstituicaoCursoOcorrenciaVM GetCursoOcorrencia(long id, long idCurso, long idOcorrencia)
        {
            return this._instituicaoRepository.GetCursoOcorrencia(id, idCurso, idOcorrencia);
        }
        [HttpGet("{id}/cursos/{idCurso}/ocorrencias/add")]
        public void AddCursoOcorrencia(long id, long idCurso, [FromBody] InstituicaoCursoOcorrenciaVM viewModel)
        {
            this._instituicaoRepository.AddCursoOcorrencia(id, idCurso, viewModel);
        }
        [HttpGet("{id}/cursos/{idCurso}/ocorrencias/del")]
        public void DeleteCursoOcorrencia(long id, long idCurso, [FromQuery] long idOcorrencia)
        {
            this._instituicaoRepository.DeleteCursoOcorrencia(id, idCurso, idOcorrencia);
        }
    }
}