using Biblioteca.Emprestimo.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Emprestimo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmprestimoController : ControllerBase
    {
        private readonly ServEmprestimo _servEmprestimo;

        public EmprestimoController(ServEmprestimo servEmprestimo)
        {
            _servEmprestimo = servEmprestimo;
        }

        [HttpPost("iniciar")]
        public async Task<IActionResult> IniciarEmprestimo([FromBody] EmprestimoDTO request)
        {
            var resultado = await _servEmprestimo.IniciarEmprestimo(request);
            if (resultado.Sucesso)
                return Ok(resultado);
            return BadRequest(resultado.Mensagem);
        }

        [HttpPost("devolver")]
        public async Task<IActionResult> DevolverLivro([FromBody] DevolverLivroDTO request)
        {
            var resultado = await _servEmprestimo.DevolverLivro(request);
            if (resultado.Sucesso)
                return Ok(resultado);
            return BadRequest(resultado.Mensagem);
        }

        [HttpGet("consultar/{membroId}")]
        public async Task<IActionResult> ConsultarEmprestimos(string membroId)
        {
            var emprestimos = await _servEmprestimo.ConsultarEmprestimosPorMembro(membroId);
            return Ok(emprestimos);
        }
    }
}
