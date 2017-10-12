using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Api.Data.Service;
using Api.Data.ViewModels;
using Domain.Repositories;

namespace Api.Controllers
{
    [Route("api/materia")]
    public class MateriaController : Controller
    {
        private MateriaService _materiaService;
        public MateriaController(MateriaRepository materiaRepository)
        {
            this._materiaService = new MateriaService(materiaRepository);
        }
        [HttpGet]
        public List<MateriaVM> All()
        {
            return this._materiaService.All();
        }
        [HttpGet("{id}")]
        public MateriaVM Detail(long id)
        {
            return this._materiaService.Detail(id);
        }
        [HttpPut("add")]
        public MateriaVM Add([FromBody] MateriaVM viewModel)
        {
            return this._materiaService.Add(viewModel);
        }
        [HttpPost("update")]
        public MateriaVM Update([FromBody] MateriaVM viewModel)
        {
            return this._materiaService.Update(viewModel);
        }
        [HttpDelete("disable/{id}")]
        public void Disable(long id)
        {
            this._materiaService.Delete(id);
        }
    }
}