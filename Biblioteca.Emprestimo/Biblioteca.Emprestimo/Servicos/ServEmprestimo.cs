using Biblioteca.Emprestimo.DTO;


using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Biblioteca.Emprestimo
{
    public class ServEmprestimo
    {
        private const string _livrosController = "api/Livro/";
        private const string _membrosController = "api/Membro/";
        private readonly HttpClient _httpClient;
        private readonly string _baseUrlLivros;
        private readonly string _baseUrlMembros;

        public ServEmprestimo()
        {
            _httpClient = new HttpClient();
            _baseUrlLivros = "http://localhost:5089/";  
            _baseUrlMembros = "http://localhost:5189/";  
        }

        public async Task<string> RegistrarEmprestimo(int livroId, int membroId)
        {
            // Consultar se o livro está disponível
            var livroDisponivel = await VerificarDisponibilidadeLivroAsync(livroId);
            if (!livroDisponivel)
            {
                return "O livro não está disponível para empréstimo.";
            }

            // Consultar se o membro está ativo
            var membroAtivo = await VerificarMembroAtivoAsync(membroId);
            if (!membroAtivo)
            {
                return "O membro não está ativo.";
            }

            // Registrar o empréstimo (exemplo de como salvar no banco ou fazer outra operação)
            // Aqui você pode adicionar a lógica de registro do empréstimo no banco de dados

            // Atualizar a disponibilidade do livro para "indisponível"
            await AlterarDisponibilidadeLivroAsync(livroId, false);

            return "Empréstimo registrado com sucesso!";
        }

        // Verificar a disponibilidade do livro consultando o microsserviço de Livros
        public async Task<bool> VerificarDisponibilidadeLivroAsync(int livroId)
        {
            var url = $"{_baseUrlLivros}{_livrosController}verificar-disponibilidade/{livroId}";
            var resposta = await _httpClient.GetAsync(url);

            if (resposta.IsSuccessStatusCode)
            {
                var disponibilidade = await resposta.Content.ReadFromJsonAsync<bool>();
                return disponibilidade;
            }

            return false;
        }

        // Verificar se o membro está ativo consultando o microsserviço de Membros
        public async Task<bool> VerificarMembroAtivoAsync(int membroId)
        {
            var url = $"{_baseUrlMembros}{_membrosController}verificar-ativo/{membroId}";
            var resposta = await _httpClient.GetAsync(url);

            if (resposta.IsSuccessStatusCode)
            {
                var ativo = await resposta.Content.ReadFromJsonAsync<bool>();
                return ativo;
            }

            return false;
        }

        // Alterar a disponibilidade do livro consultando o microsserviço de Livros
        public async Task AlterarDisponibilidadeLivroAsync(int livroId, bool disponibilidade)
        {
            var url = $"{_baseUrlLivros}{_livrosController}alterar-disponibilidade/{livroId}";
            var content = new StringContent(JsonSerializer.Serialize(disponibilidade), System.Text.Encoding.UTF8, "application/json");

            var resposta = await _httpClient.PostAsync(url, content);
            if (!resposta.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao atualizar a disponibilidade do livro.");
            }
        }
    }
}
