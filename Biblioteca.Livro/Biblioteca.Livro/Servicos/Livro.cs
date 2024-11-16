namespace Biblioteca
{
    public class Livro
    {
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Autor { get; set; }

        public bool Disponivel { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

    }
}
