using Biblioteca.Emprestimo;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Biblioteca.Emprestimo.DTO;

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
        
    }
}