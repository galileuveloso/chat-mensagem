namespace ChatMensagem.Domain
{
    //TODO - Mapeada para quando quiser registrar as mensagens e chats no banco
    public abstract class Entity
    {
        public int Id { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
