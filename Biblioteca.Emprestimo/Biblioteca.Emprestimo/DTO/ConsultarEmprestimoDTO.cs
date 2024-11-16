namespace Biblioteca.Emprestimo.DTO
{
    public class ConsultarEmprestimoDTO
    {
        public int Id { get; set; }
        public int LivroId { get; set; }
        public int MembroId { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime? DataDevolucao { get; set; }  // nulo caso não tenha sido devolvido ainda
    }
}
