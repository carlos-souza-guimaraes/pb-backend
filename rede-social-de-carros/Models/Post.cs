using System.ComponentModel.DataAnnotations.Schema;

namespace rede_social_de_carros.Models
{
    public class Post
    {

        public int Id { get; set; }
        public Usuario? Usuario { get; set; }
        [ForeignKey("UsuarioId")]
        public string UsuarioId { get; set; }
        public DateTime DataPublicacao { get; private set; }
        public int Curtidas { get; private set; } = 0;
        public string ImagemUri { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }

        public void AdicionarCurtida()
        {
            Curtidas++;
        }

        public void StampTime()
        {
            DataPublicacao = DateTime.Now;
        }
    }
}
