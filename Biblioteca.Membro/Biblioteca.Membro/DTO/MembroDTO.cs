namespace Biblioteca.Membros.DTO
{
    public class MembroDTO
    {
        public int Id { get; set; } 
        public string Nome { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
    }

}
