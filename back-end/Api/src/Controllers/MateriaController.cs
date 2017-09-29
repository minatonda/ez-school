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
        public List<MateriaVM> Get()
        {
            return this._materiaService.GetAll();
        }
        [HttpGet("sht/{id}")]
        public List<ShortVM> GetShort()
        {
            return this._materiaService.GetAllShort();
        }
        [HttpGet("dtl/{id}")]
        public MateriaVM GetDetail(long id)
        {
            return this._materiaService.GetDetail(id);
        }
        [HttpPut("add")]
        public MateriaVM Put([FromBody] MateriaVM viewModel)
        {
            return this._materiaService.Add(viewModel);
        }
        [HttpPost("upd")]
        public MateriaVM Post([FromBody] MateriaVM viewModel)
        {
            return this._materiaService.Update(viewModel);
        }
        [HttpDelete("del/{id}")]
        public void Delete(long id)
        {
            this._materiaService.Delete(id);
        }
    }
}