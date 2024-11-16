namespace Biblioteca.Emprestimo.DTO
{
    public class EmprestimoDTO
    {
        public int LivroId { get; set; }
        public int MembroId { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime? DataDevolucao { get; set; }
    }
}
