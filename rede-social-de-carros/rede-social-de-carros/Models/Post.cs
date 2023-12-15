namespace rede_social_de_carros.Models
{
    public class Post
    {
        public string Id { get; set; }
        public string UsuarioId { get; set; }
        public string DataPublicacao { get; set; }
        public int Curtidas { get; set; }
        public string ImagemUri { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }

        public override string ToString()
        {
            return $"\n{Id};{UsuarioId};{DataPublicacao};{Curtidas};{ImagemUri};{Titulo};{Conteudo}";
        }

    }
}
