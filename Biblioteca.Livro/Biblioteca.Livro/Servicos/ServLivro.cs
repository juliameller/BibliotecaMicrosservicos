using Biblioteca;
using Microsoft.EntityFrameworkCore;


namespace Biblioteca 
{
    public class ServLivro
    {
        public DataContext _dataContext;

        public ServLivro()
        {
            _dataContext = GeradorDeServicos.CarregarContexto();
        }

        public Livro InserirLivro(InserirLivroDTO dto)
        {
            var livro = new Livro
            {
                Titulo = dto.Titulo,
                Autor = dto.Autor,
                Disponivel = dto.Disponivel,  // disponível - 1 reservado - 0
                DataCadastro = DateTime.UtcNow
            };

            _dataContext.Livro.Add(livro);
            _dataContext.SaveChanges();

            return livro;

        }

        // alterar a disponibilidade do livro
        public bool AlterarDisponibilidade(int livroId, bool disponibilidade)
        {
            var livro = _dataContext.Livro.Find(livroId);
            if (livro == null)
            {
                throw new Exception("Livro não encontrado");
            }

            // Atualiza o campo Disponivel 
            livro.Disponivel = disponibilidade;

            _dataContext.Livro.Update(livro);
            _dataContext.SaveChanges();

            return true; 
        }

        // consultar a disponibilidade do livro
        public bool VerificarDisponibilidade(int id)
        {
            var livro = _dataContext.Livro.Find(id);
            return livro != null && livro.Disponivel == true; 
        }

        public Livro ConsultarLivroPorId(int id)
        {
            var livro = _dataContext.Livro.Find(id);
            if (livro == null)
            {
                throw new Exception("Livro não encontrado");
            }

            return livro;
        }
    }
}

