using Biblioteca.Membros.DTO;


namespace Biblioteca.Membros
{
    public class ServMembro
    {
        public DataContext _dataContext;

        public ServMembro()
        {
            _dataContext = GeradorDeServicos.CarregarContexto();
        }

        public Membro InserirMembro(InserirMembroDTO dto)
        {
            var membro = new Membro
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Ativo = dto.Ativo, // ativo - 1 desativado - 0
                DataCadastro = DateTime.UtcNow
            };

            _dataContext.Membro.Add(membro);
            _dataContext.SaveChanges();

            return membro; 
        }

        public bool ConsultarDisponibilidade(int id)
        {
            var membro = _dataContext.Membro.Find(id);
            return membro != null && membro.Ativo; 
        }

    }
}
