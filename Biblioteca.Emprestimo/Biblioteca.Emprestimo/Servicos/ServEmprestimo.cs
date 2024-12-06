using System.Text.Json;
using System.Net.Http.Json;
using Biblioteca.Emprestimo.DTO;

namespace Biblioteca.Emprestimo.Services
{
    public class ServEmprestimo
    {
        private const string _livrosController = "api/Livro/";
        private const string _membrosController = "api/Membro/";
        private readonly HttpClient _httpClient;

        public ServEmprestimo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Método para iniciar um empréstimo
        public async Task<ResultadoDTO> IniciarEmprestimo(EmprestimoDTO dto)
        {
            // Verificar disponibilidade do livro
            if (!await VerificarDisponibilidadeLivroAsync(dto.LivroId))
                return new ResultadoDTO(false, "Livro não está disponível para empréstimo.");

            // Verificar se o membro está ativo
            if (!await VerificarMembroAtivoAsync(dto.MembroId))
                return new ResultadoDTO(false, "Membro não está ativo.");

            // Alterar a disponibilidade do livro
            await AlterarDisponibilidadeLivroAsync(dto.LivroId, false);

            // Retornar sucesso
            return new ResultadoDTO(true, "Empréstimo iniciado com sucesso!");
        }

        // Método para devolver um livro
        public async Task<ResultadoDTO> DevolverLivro(DevolverLivroDTO dto)
        {
            // Alterar a disponibilidade do livro para disponível
            await AlterarDisponibilidadeLivroAsync(dto.LivroId, true);

            // Retornar sucesso
            return new ResultadoDTO(true, "Livro devolvido com sucesso!");
        }

        // Método para consultar empréstimos por membro
        public async Task<List<EmprestimoDTO>> ConsultarEmprestimosPorMembro(int membroId)
        {
            // Simulação de consulta - ajuste para buscar de um banco ou outro sistema.
            return new List<EmprestimoDTO>
            {
                new EmprestimoDTO
                {
                    LivroId = 1,
                    MembroId = membroId,
                    DataEmprestimo = DateTime.UtcNow
                }
            };
        }

        // Método auxiliar para verificar a disponibilidade de um livro
        private async Task<bool> VerificarDisponibilidadeLivroAsync(int livroId)
        {
            var resposta = await _httpClient.GetAsync($"{_livrosController}disponibilidade/{livroId}");
            if (resposta.IsSuccessStatusCode)
            {
                var resultado = await resposta.Content.ReadFromJsonAsync<bool>();
                return resultado;
            }

            return false;
        }

        // Método auxiliar para verificar se o membro está ativo
        private async Task<bool> VerificarMembroAtivoAsync(int membroId)
        {
            var resposta = await _httpClient.GetAsync($"{_membrosController}disponibilidade/{membroId}");
            if (resposta.IsSuccessStatusCode)
            {
                var resultado = await resposta.Content.ReadFromJsonAsync<bool>();
                return resultado;
            }

            return false;
        }

        // Método auxiliar para alterar a disponibilidade de um livro
        private async Task AlterarDisponibilidadeLivroAsync(int livroId, bool disponibilidade)
        {
            var content = JsonContent.Create(disponibilidade);
            var resposta = await _httpClient.PutAsync($"{_livrosController}alterar-disponibilidade/{livroId}", content);
            if (!resposta.IsSuccessStatusCode)
                throw new Exception("Erro ao alterar a disponibilidade do livro.");
        }
    }
}
