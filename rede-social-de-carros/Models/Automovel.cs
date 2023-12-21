using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace rede_social_de_carros.Models
{
    public class Automovel
    {

        public int Id { get; set; }
        public Usuario? Usuario { get; set; }
        [ForeignKey("UsuarioId")]
        public string UsuarioId { get; set; }
        public string NomeAutomovel { get; set; }
        public string Fabricante { get; set; }
        public string Modelo { get; set; }
        public int AnoDoAutomovel { get; set; }
        public double Valor { get; set; }
        public bool Original { get; set; }

        public string ImageUri { get; set; }

    }
}
