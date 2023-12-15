using rede_social_de_carros.Models;

public static class UsuarioService
{
    private static List<Usuario> Usuarios = new List<Usuario>();

    public static List<Usuario> ObterLista()
    {
        return Usuarios;
    }

    public static void Incluir(Usuario usuario)
    {
        Usuarios.Add(usuario);
    }
}