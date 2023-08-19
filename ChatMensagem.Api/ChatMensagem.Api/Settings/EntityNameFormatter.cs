using MassTransit;

namespace ChatMensagem.Api.Settings
{
    public class EntityNameFormatter : IEntityNameFormatter
    {
        private readonly IEntityNameFormatter _original;
        private readonly string _prefix;

        public EntityNameFormatter
        (
            IEntityNameFormatter original,
            string prefix
        )
        {
            if (string.IsNullOrWhiteSpace(prefix))
                throw new ArgumentNullException($"Queue não configurada => {nameof(prefix)}");

            _original = original;
            _prefix = prefix;
        }

        public string FormatEntityName<T>()
        {
            var consumerFullName = _original.FormatEntityName<T>();
            var consumerName = consumerFullName.Split(":").Last();
            return $"{_prefix}-{consumerName}";
        }
    }
}
