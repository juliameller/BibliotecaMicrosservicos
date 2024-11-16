using Microsoft.AspNetCore.Mvc;

namespace Biblioteca
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private ServLivro _servLivro;

        public LivroController()
        {
            _servLivro = new ServLivro();
        }

        // Endpoint para cadastrar um livro
        [HttpPost]
        public ActionResult InserirLivro([FromBody] InserirLivroDTO dto)
        {
            try
            {
                //_servLivro.InserirLivro(dto);
                var livroInserido = _servLivro.InserirLivro(dto);
                return CreatedAtAction(nameof(ConsultarLivroPorId), new { id = livroInserido.Id }, livroInserido);
                //return Ok("Livro inserido com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // Endpoint para consultar um livro
        [HttpGet("consultar/{id}")]
        public ActionResult ConsultarLivroPorId(int id)
        {
            try
            {
                var livro = _servLivro.ConsultarLivroPorId(id);

                return Ok(livro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Endpoint para alterar a disponibilidade do livro
        [HttpPut("alterar-disponibilidade/{id}")]
        public ActionResult AlterarDisponibilidade(int id, [FromBody] bool disponibilidade)
        {
            bool resultado = _servLivro.AlterarDisponibilidade(id, disponibilidade);
            if (resultado)
            {
                return Ok("Disponibilidade do livro atualizada com sucesso.");
            }
            else
            {
                return NotFound("Livro não encontrado.");
            }
        }

        // Endpoint para verificar se o livro está disponível
        [HttpGet("disponibilidade/{id}")]
        public ActionResult VerificarDisponibilidade(int id)
        {
            bool disponivel = _servLivro.VerificarDisponibilidade(id);
            return Ok(new { LivroId = id, Disponivel = disponivel });
        }
    }
}
