using Microsoft.Extensions.Caching.Memory;
using Biblioteca.Emprestimo.DTO;

namespace Biblioteca.Emprestimo
{
    public class LivroHelper
    {

        private const string _livroController = "api/Livro/";
        private IMemoryCache _memoryCache;

        public LivroHelper()
        {
            _memoryCache = GeradorDeServicos.CarregarServicoDeCache();
        }

        public LivroDTO RetornarLivro(int livroId)
        {
            var httpClient = new HttpClient();
            var urlLivro = BuscarUrlLivro();
            var url = urlLivro + _livroController + livroId;

            var resposta = httpClient.GetAsync(url).Result;

            if (!resposta.IsSuccessStatusCode)
            {
                throw new Exception("Livro " + livroId + " não encontrado.");
            }

            var livro = resposta.Content.ReadFromJsonAsync<LivroDTO>().Result;

            InserirLivroNoCache(livro);

            return livro;
        }

        public void InserirLivroNoCache(LivroDTO livroDto)
        {
            _memoryCache.Set("Livro" + livroDto.Id, livroDto, TimeSpan.FromHours(1));
        }

        public LivroDTO RetornarLivroComCache(int livroId)
        {
            var livro = _memoryCache.Get<LivroDTO>("Livro" + livroId);

            if (livro != null)
            {
                return livro;
            }

            livro = RetornarLivro(livroId);
            return livro;
        }

        public string BuscarUrlLivro()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            string url = configuration["UrlBiblioteca"];
            return url;
        }
    }
}
