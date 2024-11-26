using AgroVisitAPI.Models;
using AutoMapper;
using Core;
using Core.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AgroVisitAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitaController : ControllerBase
    {
        private readonly IVisitaService _visitaService;
        private readonly IMapper _mapper;

        public VisitaController(IVisitaService visitaService, IMapper mapper)
        {
            _visitaService = visitaService;
            _mapper = mapper;
        }

        // GET: api/<VisitaController>
        [HttpGet]
        public ActionResult Get()
        {
            var listaVisitas = _visitaService.GetAll();
            if (listaVisitas == null)
            {
                return NotFound();
            }
            return Ok(listaVisitas);
        }

        // GET api/<VisitaController>/5
        [HttpGet("{id}")]
        public ActionResult Get(uint id)
        {
            Visita visita = _visitaService.Get(id);
            if (visita == null)
            {
                return NotFound();
            }
            return Ok(visita);
        }

        // POST api/<VisitaController>
        [HttpPost]
        public ActionResult Post([FromBody] VisitaViewModel visitaModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Dados inválidos.");

            var visita = _mapper.Map<Visita>(visitaModel);
            _visitaService.Create(visita);

            return Ok();
        }

        // PUT api/<VisitaController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] VisitaViewModel visitaModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Dados inválidos.");

            var visita = _mapper.Map<Visita>(visitaModel);
            if (visita == null)
                return NotFound();

            _visitaService.Edit(visita);

            return Ok();
        }

        // DELETE api/<VisitaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(uint id)
        {
            Visita visita = _visitaService.Get(id);
            if (visita == null)
                return NotFound();

            _visitaService.Delete(id);
            return Ok();
        }
    }
}
