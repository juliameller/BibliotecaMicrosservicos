using Microsoft.Extensions.Caching.Memory;
using Biblioteca.Emprestimo.DTO;

namespace Biblioteca.Emprestimo
{
    public class MembroHelper
    {
        private const string _membroController = "api/Membro/";
        private IMemoryCache _memoryCache;

        public MembroHelper()
        {
            _memoryCache = GeradorDeServicos.CarregarServicoDeCache();
        }

        public MembroDTO RetornarMembro(int membroId)
        {
            var httpClient = new HttpClient();
            var urlMembro = BuscarUrlMembro();
            var url = urlMembro + _membroController + membroId;

            var resposta = httpClient.GetAsync(url).Result;

            if (!resposta.IsSuccessStatusCode)
            {
                throw new Exception("Membro " + membroId + " não encontrado.");
            }

            var membro = resposta.Content.ReadFromJsonAsync<MembroDTO>().Result;

            InserirMembroNoCache(membro);

            return membro;
        }

        public void InserirMembroNoCache(MembroDTO membroDto)
        {
            _memoryCache.Set("Membro" + membroDto.Id, membroDto, TimeSpan.FromHours(1));
        }

        public MembroDTO RetornarMembroComCache(int membroId)
        {
            var membro = _memoryCache.Get<MembroDTO>("Membro" + membroId);

            if (membro != null)
            {
                return membro;
            }

            membro = RetornarMembro(membroId);
            return membro;
        }

        public string BuscarUrlMembro()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            string url = configuration["UrlMembro"];
            return url;
        }
    }
}
