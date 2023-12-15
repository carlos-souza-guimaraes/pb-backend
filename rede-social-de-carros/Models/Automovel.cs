using System.Collections.Generic;

namespace rede_social_de_carros.Models
{
    public class Automovel{

        public string Id { get; set; }
        public string UsuarioId { get; set; }
        public string NomeAutomovel { get; set; }
        public string Fabricante { get; set; }
        public string Modelo { get; set; }
        public int AnoDoAutomovel { get; set; }
        public double Valor { get; set; }
        public bool Original { get; set; }
        public Dictionary<string, string> Pecas { get; set; }
        public List<string> ImageUris { get; set; }

        public override string ToString()
        {
            string dictionaryString = ConvertDictionaryToString(Pecas);
            string listString = ConvertListToString(ImageUris);

            return $"\n{Id};{UsuarioId};{NomeAutomovel};{Fabricante};{Modelo};{AnoDoAutomovel};{Valor};{Original};{dictionaryString};{listString}";
        }

        private string ConvertDictionaryToString(Dictionary<string, string> dictionary)
        {
            if (dictionary.Count == 0)
            {
                return "";
            }
            string result = string.Join(", ", dictionary.Select(kv => $"{kv.Key}:{kv.Value}"));
            return result;
        }

        private string ConvertListToString(List<string> list)
        {
            if (list.Count == 0)
            {
                return "";
            }
            string result = string.Join(", ", list);
            return result;
        }


    }
}
