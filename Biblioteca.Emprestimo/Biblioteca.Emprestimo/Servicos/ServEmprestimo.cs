public class ServEmprestimo
{
    private const string _livrosController = "api/Livro/";
    private const string _membrosController = "api/Membro/";
    private readonly HttpClient _httpClient;

    public ServEmprestimo(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(configuration["BaseUrls:ApiGateway"]);
    }

    public async Task<string> RegistrarEmprestimo(int livroId, int membroId)
    {
        // Verifica disponibilidade do livro
        var livroDisponivel = await VerificarDisponibilidadeLivroAsync(livroId);
        if (!livroDisponivel)
            return "O livro não está disponível para empréstimo.";

        // Verifica se membro está ativo
        var membroAtivo = await VerificarMembroAtivoAsync(membroId);
        if (!membroAtivo)
            return "O membro não está ativo.";

        // Atualiza disponibilidade
        await AlterarDisponibilidadeLivroAsync(livroId, false);

        return "Empréstimo registrado com sucesso!";
    }

    public async Task<bool> VerificarDisponibilidadeLivroAsync(int livroId)
    {
        var resposta = await _httpClient.GetAsync($"{_livrosController}verificar-disponibilidade/{livroId}");
        if (resposta.IsSuccessStatusCode)
            return await resposta.Content.ReadFromJsonAsync<bool>();

        return false;
    }

    public async Task<bool> VerificarMembroAtivoAsync(int membroId)
    {
        var resposta = await _httpClient.GetAsync($"{_membrosController}verificar-ativo/{membroId}");
        if (resposta.IsSuccessStatusCode)
            return await resposta.Content.ReadFromJsonAsync<bool>();

        return false;
    }

    public async Task AlterarDisponibilidadeLivroAsync(int livroId, bool disponibilidade)
    {
        var content = new StringContent(JsonSerializer.Serialize(disponibilidade), System.Text.Encoding.UTF8, "application/json");
        var resposta = await _httpClient.PostAsync($"{_livrosController}alterar-disponibilidade/{livroId}", content);

        if (!resposta.IsSuccessStatusCode)
            throw new Exception("Erro ao atualizar a disponibilidade do livro.");
    }
}
