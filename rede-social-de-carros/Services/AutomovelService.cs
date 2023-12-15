using rede_social_de_carros.Models;

public static class AutomovelService
{
    private static List<Automovel> Automoveis = new List<Automovel>();

    public static List<Automovel> ObterLista()
    {
        return Automoveis;
    }

    public static void Incluir(Automovel automovel)
    {
        Automoveis.Add(automovel);
    }
}