using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Identity;

namespace rede_social_de_carros.Models
{
    public class Usuario : IdentityUser
    {
        [ForeignKey("EnderecoId")]
        public int? EnderecoId { get; set; }
        public Endereco? Endereco { get; set; }
        public ICollection<Post> PostList { get; set; }
        public ICollection<Automovel> Automoveis { get; set; }

    }
}
