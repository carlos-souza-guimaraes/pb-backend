using rede_social_de_carros.Models;

namespace rede_social_de_carros.Services
{
    public class EnderecoService
    {
        private readonly SeuDbContext _dbContext;

        public EnderecoService(SeuDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task IncluirEndereco(Endereco endereco)
        {
            _dbContext.Enderecos.Add(endereco);
            await _dbContext.SaveChangesAsync();
        }
    }
}
