using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Siace.Data.Models;
using Siace.Data.Repository;


namespace Siace.WS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreguntasController : ControllerBase
    {
        private IPreguntasRepository _preguntasRepository;

        public PreguntasController(IPreguntasRepository preguntasRepository)
        {   
            _preguntasRepository = preguntasRepository;
        }    


        [HttpGet("GetQuestion")]
        public async Task<IActionResult> GetQuestions(int id)
        {
            var res = await this._preguntasRepository.Get(id);
            return Ok(res);
        }

        [HttpGet("Allquestions")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetPreguntasWithRespuestas([FromQuery] int conId)
        {
            try
            {
                var preguntas = await _preguntasRepository.GetAllQuestions(conId);
                return Ok(preguntas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("guardar-contestaciones-respuestas")]
        public async Task<ActionResult> GuardarContestacionesRespuestas(IEnumerable<Preguntaa> questionsData, [FromQuery] int corConId)
        {
            try
            {
                var result =  _preguntasRepository.CreateUpdate(questionsData, corConId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
