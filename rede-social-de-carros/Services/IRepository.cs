namespace rede_social_de_carros.Services
{
    public interface IRepository<T>
    {
        public Task<List<T>> ObterTodos();
        public Task<T> ObterPorId(int id);
        public Task<T> ObterPorId(string id);
        public Task<T> Adicionar(T obj);
        public Task<T> Editar(T obj);
        public Task Deletar(T obj);
    }
}
