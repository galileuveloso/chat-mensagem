namespace ChatMensagem.Domain
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public Guid IdExterno { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
