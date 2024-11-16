namespace Biblioteca.Emprestimo
{
    public class Emprestimo
    {
        public int Id { get; set; }
        public int LivroId { get; set; }  
        public int MembroId { get; set; }
        public DateTime DataEmpréstimo { get; set; }
        public DateTime? DataDevolução { get; set; }

    }
}
