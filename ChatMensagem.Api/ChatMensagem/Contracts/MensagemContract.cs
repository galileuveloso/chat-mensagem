namespace ChatMensagem.Contracts
{
    public class MensagemContract : BaseContract
    {
        public string Mensagem { get; set; }
        public string Usuario { get; set; }
        public Guid ChatId { get; set; }
    }
}
