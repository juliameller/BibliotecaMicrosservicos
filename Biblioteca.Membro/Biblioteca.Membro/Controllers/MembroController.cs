using Microsoft.AspNetCore.Mvc;
using Biblioteca.Membros.DTO;

namespace Biblioteca.Membros
{

    [Route("api/[controller]")]
    [ApiController]
    public class MembroController : ControllerBase
    {
        private ServMembro _servMembro;

        public MembroController()
        {
            _servMembro = new ServMembro();
        }

        [HttpPost("cadastrar")]
        public ActionResult CadastrarMembro([FromBody] InserirMembroDTO dto)
        {
            var membro = _servMembro.InserirMembro(dto);
            return CreatedAtAction(nameof(ConsultarDisponibilidade), new { id = membro.Id }, membro);
        }

        [HttpGet("disponibilidade/{id}")]
        public ActionResult ConsultarDisponibilidade(int id)
        {
            var disponibilidade = _servMembro.ConsultarDisponibilidade(id);
            if (!disponibilidade)
            {
                return NotFound("Membro não encontrado ou não está ativo.");
            }

            return Ok("Membro ativo.");
        }
    }
}
