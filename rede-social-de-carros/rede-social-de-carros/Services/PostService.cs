using rede_social_de_carros.Models;

public static class PostService
{
    private static List<Post> Posts = new List<Post>();

    public static List<Post> ObterLista()
    {
        return Posts;
    }

    public static void Incluir(Post post)
    {
        Posts.Add(post);
    }

}