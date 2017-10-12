using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Api.Data.Service;
using Api.Data.ViewModels;
using Domain.Repositories;

namespace Api.Controllers
{
    [Route("api/curso")]
    public class CursoController : Controller
    {

        private CursoService _cursoService;
        public CursoController(CursoRepository cursoRepository)
        {
            this._cursoService = new CursoService(cursoRepository);
        }
        [HttpGet]
        public List<CursoVM> All()
        {
            return this._cursoService.All();
        }
        [HttpGet("{id}")]
        public CursoVM Detail(long id)
        {
            return this._cursoService.Detail(id);
        }
        [HttpPut("add")]
        public CursoVM Add([FromBody] CursoVM viewModel)
        {
            return this._cursoService.Add(viewModel);
        }
        [HttpPost("update")]
        public CursoVM Update([FromBody] CursoVM viewModel)
        {
            return this._cursoService.Update(viewModel);
        }
        [HttpDelete("disable")]
        public void Disable([FromQuery] long id)
        {
            this._cursoService.Disable(id);
        }

        [HttpGet("{id}/grade")]
        public List<CursoGradeVM> AllGrade(long id)
        {
            return this._cursoService.AllGrade(id);
        }
        [HttpGet("{id}/grade/{descricao}")]
        public CursoGradeVM DetailGrade(long id, long idCursoGrade)
        {
            return this._cursoService.DetailGrade(id, idCursoGrade);
        }
        [HttpPut("{id}/grade/add")]
        public CursoGradeVM AddGrade(long id, [FromBody] CursoGradeVM model)
        {
            return this._cursoService.AddGrade(id, model);
        }
        [HttpPost("{id}/grade/update")]
        public CursoGradeVM UpdateGrade(long id, [FromBody] CursoGradeVM model)
        {
            return this._cursoService.UpdateGrade(id, model);
        }
        [HttpDelete("{id}/grade/disable")]
        public void DisableGrade(long id, [FromQuery]long idCursoGrade)
        {
            this._cursoService.DeleteGrade(id, idCursoGrade);
        }

    }
}