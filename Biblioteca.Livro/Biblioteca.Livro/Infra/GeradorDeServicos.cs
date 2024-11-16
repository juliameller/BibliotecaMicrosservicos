using Microsoft.Extensions.Caching.Memory;

namespace Biblioteca
{
    public static class GeradorDeServicos
    {
        public static ServiceProvider ServiceProvider;

        public static DataContext CarregarContexto()
        {
            return ServiceProvider.GetService<DataContext>();
        }

        public static IMemoryCache CarregarServicoDeCache()
        {
            return ServiceProvider.GetService<IMemoryCache>();
        }
    }
}
