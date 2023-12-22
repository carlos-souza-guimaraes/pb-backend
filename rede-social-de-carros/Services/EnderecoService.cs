using Microsoft.EntityFrameworkCore;
using rede_social_de_carros.Data;
using rede_social_de_carros.Models;

namespace rede_social_de_carros.Services
{
    public class EnderecoService : IRepository<Endereco>
    {
        private readonly ApplicationDbContext _context;

        public EnderecoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Endereco>> ObterTodos()
        {
            return await _context.Endereco
            .ToListAsync();
        }

        public async Task<Endereco> ObterPorId(int id)
        {
            return await _context.Endereco
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task<Endereco> ObterPorId(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Endereco> Adicionar(Endereco obj)
        {
            var newObj = _context.AddAsync(obj);
            _context.SaveChanges();
            return newObj.Result.Entity;
        }

        public Task Deletar(Endereco obj)
        {
            var result = _context.Remove(obj);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<Endereco> Editar(Endereco obj)
        {
            var editedObj = _context.Update(obj);
            _context.SaveChanges();
            return editedObj.Entity;
        }

    }
}
