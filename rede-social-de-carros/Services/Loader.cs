using rede_social_de_carros.Models;
using System.Linq;

public static class Loader
{

    public static void ObterUsuarios()
    {
        var data = File.ReadAllText("./Data/Usuarios.txt");
        var items = data.Split('\n');
        foreach (var item in items)
        {
            var values = item.Split(';');
            var usuario = new Usuario
            {
                Id = values[0],
                NomeUsuario = values[1],
                SenhaUsuario = values[2],
                Email = values[3],
                PostList = PopularPosts(values[0]),
                Automoveis = PopularAutomoveis(values[0]),
            };

            UsuarioService.Incluir(usuario);
        }
    }

    public static List<Post> PopularPosts(string id)
    {
        return PostService.ObterLista().Where(p => p.UsuarioId == id).ToList();
    }

    public static List<Automovel> PopularAutomoveis(string id)
    {
        return AutomovelService.ObterLista().Where(a => a.UsuarioId == id).ToList();
    }

    public static void ObterAutomoveis()
    {
        var data = File.ReadAllText("./Data/Automoveis.txt");
        var items = data.Split('\n');
        foreach (var item in items)
        {
            var values = item.Split(';');
            var automovel = new Automovel
            {
                Id = values[0],
                UsuarioId = values[1],
                NomeAutomovel = values[2],
                Fabricante = values[3],
                Modelo = values[4],
                AnoDoAutomovel = Int32.Parse(values[5]),
                Valor = Int32.Parse(values[6]),
                Original = bool.Parse(values[7]),
                Pecas = PopularPecas(values[8]),
                ImageUris = PopularImagens(values[9])
            };

            AutomovelService.Incluir(automovel);
        }
    }

    public static Dictionary<string, string> PopularPecas(string pecas)
    {
        var pecasDict = new Dictionary<string, string>();
        var items = pecas.Split(",");
        foreach (var item in items)
        {
            var parts = item.Split(':');
            if(parts.Length == 2)
            {
                string key = parts[0].Trim();
                string value = parts[1].Trim();
                pecasDict[key] = value;
            }
        }
        return pecasDict;
    }

    public static List<string> PopularImagens(string uris)
    {
        return uris.Split(",").ToList();
    }

    public static void ObterPosts()
    {

        var data = File.ReadAllText("./Data/Posts.txt");
        var items = data.Split('\n');
        foreach (var item in items)
        {
            var values = item.Split(';');
            var post = new Post
            {
                Id = values[0],
                UsuarioId = values[1],
                DataPublicacao = values[2],
                Curtidas = Int32.Parse(values[3]),
                ImagemUri = values[4],
                Titulo = values[5],
                Conteudo = values[6]
            };

            PostService.Incluir(post);
        }
    }

    public static void AdicionarUsuario(Usuario usuario)
    {
        File.AppendAllText("./Data/Usuarios.txt", usuario.ToString());
    }

    public static void AdicionarAutomovel(Automovel automovel)
    {
        File.AppendAllText("./Data/Automoveis.txt", automovel.ToString());
    }

    public static void AdicionarPost(Post post)
    {
        File.AppendAllText("./Data/Posts.txt", post.ToString());
    }


}