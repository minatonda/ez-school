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
        [HttpGet("sht")]
        public List<ShortVM> GetShort()
        {
            return this._cursoService.GetAllShort();
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
        [HttpGet("{id}/grades/{idGrade}")]
        public CursoGradeVM GetGradeDetail(long id, long idGrade)
        {
            return this._cursoService.GetGradeDetail(id, idGrade);
        }
        [HttpPut("{id}/grades/add")]
        public CursoGradeVM AddGrades(long id, [FromBody] CursoGradeVM model)
        {
            return this._cursoService.AddGrades(id, model);
        }
        [HttpDelete("{id}/grades/del")]
        public void DeleteGrades(long id, [FromQuery]long idGrade)
        {
            this._cursoService.DeleteGrades(id, idGrade);
        }

        [HttpGet("{id}/grades/{idGrade}/materias")]
        public List<MateriaVM> GetGradeMaterias(long id, long idGrade)
        {
            return this._cursoService.GetGradeMaterias(id, idGrade);
        }
        [HttpPut("{id}/grades/{idGrade}/materias/add")]
        public void AddGradeMaterias(long id, long idGrade, [FromBody] MateriaVM model)
        {
            this._cursoService.AddGradeMaterias(id, idGrade, model);
        }
        [HttpDelete("{id}/grades/{idGrade}/materias/del")]
        public void DeleteGradeMaterias(long id, long idGrade, [FromQuery]long idMateria)
        {
            this._cursoService.DeleteGradeMaterias(id, idGrade, idMateria);
        }


    }
}