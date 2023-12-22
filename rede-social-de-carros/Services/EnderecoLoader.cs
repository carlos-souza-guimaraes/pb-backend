namespace rede_social_de_carros.Services
{
    public class EnderecoLoader
    {
        private readonly ViaCepClient _viaCepClient;
        private readonly EnderecoService _enderecoService;

        public EnderecoLoader(ViaCepClient viaCepClient, EnderecoService enderecoService)
        {
            _viaCepClient = viaCepClient;
            _enderecoService = enderecoService;
        }

        public async Task IncluirEnderecoPorCep(string cep)
        {
            var endereco = await _viaCepClient.ConsultarCep(cep);
            await _enderecoService.IncluirEndereco(endereco);
        }
    }
}
