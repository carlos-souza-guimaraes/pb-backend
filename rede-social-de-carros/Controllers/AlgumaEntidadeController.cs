using Microsoft.AspNetCore.Mvc;
using rede_social_de_carros.Models;
using System.Diagnostics;

namespace rede_social_de_carros.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlgumaEntidadeController : ControllerBase
    {
        private readonly SeuDbContext _dbContext;

        public AlgumaEntidadeController(SeuDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("algumaentidade/{id}")]
        public async Task<IActionResult> ObterAlgumaEntidade(int id)
        {
            var algumaEntidade = await _dbContext.AlgumaEntidades
                .Include(a => a.Endereco) // Inclua outras entidades associadas se necessário
                .FirstOrDefaultAsync(a => a.Id == id);

            if (algumaEntidade == null)
                return NotFound();

            return Ok(algumaEntidade);
        }
    }
}