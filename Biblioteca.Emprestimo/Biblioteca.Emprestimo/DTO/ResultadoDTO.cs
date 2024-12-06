namespace Biblioteca.Emprestimo.DTO
{
    public class ResultadoDTO
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }

        // Construtor para inicializar os valores de sucesso e mensagem
        public ResultadoDTO(bool sucesso, string mensagem)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
        }
    }
}
