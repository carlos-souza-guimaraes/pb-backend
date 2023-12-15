using System.Reflection.Metadata.Ecma335;

namespace rede_social_de_carros.Models
{
    public class Usuario
    {
        public string Id { get; set; }
        public string NomeUsuario { get; set; }
        public string SenhaUsuario { get; set; }
        public string Email { get; set; }
        public List<Post> PostList { get; set;}
        public List<Automovel> Automoveis { get; set; }

        public override string ToString()
        {
            return $"\n{Id};{NomeUsuario};{SenhaUsuario};{Email};;";
        }
    }
}
