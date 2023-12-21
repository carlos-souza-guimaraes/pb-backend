using Microsoft.EntityFrameworkCore;
using rede_social_de_carros.Data;
using rede_social_de_carros.Models;
using rede_social_de_carros.Services;

public class AutomovelService : IRepository<Automovel>
{
    private static List<Automovel> Automoveis = new List<Automovel>();

    private readonly ApplicationDbContext _context;
    private static List<Post> Posts = new List<Post>();

    public AutomovelService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Automovel>> ObterTodos()
    {
        return await _context.Automoveis
            .Include(p => p.Usuario)
            .ToListAsync();
    }
    public async Task<Automovel> ObterPorId(int id)
    {
        return await _context.Automoveis
            .Include(p => p.Usuario)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Automovel> ObterPorId(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<Automovel> Adicionar(Automovel obj)
    {
        var newObj = _context.AddAsync(obj);
        _context.SaveChanges();
        return newObj.Result.Entity;
    }

    public Task Deletar(Automovel obj)
    {
        var result = _context.Remove(obj);
        _context.SaveChanges();
        return Task.CompletedTask;
    }

    public async Task<Automovel> Editar(Automovel obj)
    {
        var editedObj = _context.Update(obj);
        _context.SaveChanges();
        return editedObj.Entity;
    }



}