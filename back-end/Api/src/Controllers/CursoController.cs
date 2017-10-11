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
        public List<CursoVM> Get()
        {
            return this._cursoService.GetAll();
        }
        [HttpGet("{id}")]
        public CursoVM GetDetail(long id)
        {
            return this._cursoService.GetDetail(id);
        }
        [HttpPut("add")]
        public CursoVM Put([FromBody] CursoVM viewModel)
        {
            return this._cursoService.Add(viewModel);
        }
        [HttpPost("upd")]
        public CursoVM Post([FromBody] CursoVM viewModel)
        {
            return this._cursoService.Update(viewModel);
        }
        [HttpDelete("del")]
        public void Delete([FromQuery] long id)
        {
            this._cursoService.Delete(id);
        }

        [HttpGet("{id}/grades")]
        public List<CursoGradeVM> GetGrades(long id)
        {
            return this._cursoService.GetGrades(id);
        }
        [HttpGet("{id}/grades/{descricao}")]
        public CursoGradeVM GetGradeDetail(long id, long idCursoGrade)
        {
            return this._cursoService.GetGradeDetail(id, idCursoGrade);
        }
        [HttpPut("{id}/grades/add")]
        public CursoGradeVM AddGrades(long id, [FromBody] CursoGradeVM model)
        {
            return this._cursoService.AddGrades(id, model);
        }
        [HttpPost("{id}/grades/upd")]
        public CursoGradeVM UpdGrades(long id, [FromBody] CursoGradeVM model)
        {
            return this._cursoService.UpdateGrades(id, model);
        }
        [HttpDelete("{id}/grades/del")]
        public void DeleteGrades(long id, [FromQuery]long idCursoGrade)
        {
            this._cursoService.DeleteGrades(id, idCursoGrade);
        }

    }
}