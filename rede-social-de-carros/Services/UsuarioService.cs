using Microsoft.EntityFrameworkCore;
using rede_social_de_carros.Data;
using rede_social_de_carros.Models;
using rede_social_de_carros.Services;

public class UsuarioService : IRepository<Usuario>
{
    private readonly ApplicationDbContext _context;
    private static List<Usuario> Usuarios = new List<Usuario>();

    public UsuarioService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Usuario>> ObterTodos()
    {
        return await _context.Users
            .Include(u => u.Automoveis)
            .Include(u => u.PostList)
            .ToListAsync();
    }

    public async Task<Usuario> ObterPorId(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Usuario> ObterPorId(string id)
    {
        return await _context.Users
            .Include(u => u.Automoveis)
            .Include(u => u.PostList)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Usuario> Adicionar(Usuario obj)
    {
        var newObj = _context.AddAsync(obj);
        _context.SaveChanges();
        return newObj.Result.Entity;
    }

    public async Task<Usuario> Editar(Usuario obj)
    {
        var editedObj = _context.Update(obj);
        _context.SaveChanges();
        return editedObj.Entity;
    }

    public Task Deletar(Usuario obj)
    {
        var result = _context.Remove(obj);
        _context.SaveChanges();
        return Task.CompletedTask;
    }
}