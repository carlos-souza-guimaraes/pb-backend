using Microsoft.EntityFrameworkCore;
using rede_social_de_carros.Data;
using rede_social_de_carros.Models;
using rede_social_de_carros.Services;

public class PostService : IRepository<Post>
{
    private readonly ApplicationDbContext _context;
    private static List<Post> Posts = new List<Post>();

    public PostService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Post>> ObterTodos()
    {
        return await _context.Posts
            .Include(p => p.Usuario)
            .ToListAsync();
    }

    public async Task<Post> ObterPorId(int id)
    {
        return await _context.Posts
            .Include(p => p.Usuario)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Post> Adicionar(Post obj)
    {
        var newObj = _context.AddAsync(obj);
        _context.SaveChanges();
        return newObj.Result.Entity;
    }

    public async Task<Post> Editar(Post obj)
    {
        var editedObj = _context.Update(obj);
        _context.SaveChanges();
        return editedObj.Entity;
    }

    public Task Deletar(Post obj)
    {
        var result = _context.Remove(obj);
        _context.SaveChanges();
        return Task.CompletedTask;
    }

    public Task<Post> ObterPorId(string id)
    {
        throw new NotImplementedException();
    }
}