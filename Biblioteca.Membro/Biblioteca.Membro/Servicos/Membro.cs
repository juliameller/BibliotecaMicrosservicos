namespace Biblioteca.Membros
{
    public class Membro
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public bool Ativo { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

    }
}
