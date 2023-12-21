using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Identity;

namespace rede_social_de_carros.Models
{
    public class Usuario : IdentityUser
    {
        public string Endereco { get; set; } = string.Empty;
        public ICollection<Post> PostList { get; set; }
        public ICollection<Automovel> Automoveis { get; set; }

    }
}
