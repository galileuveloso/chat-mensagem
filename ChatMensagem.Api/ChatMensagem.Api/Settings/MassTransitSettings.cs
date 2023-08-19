using MassTransit;

namespace ChatMensagem.Api.Settings
{
    public class MassTransitSettings
    {
        public const string ConfigurationSection = "masstransit";

        public string Server { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Prefix { get; set; }
        public string Environment { get; set; }
        public string VirtualHost { get { return GetVirtualHost(); } set => _virtualHost = value; }

        private string _virtualHost;
        private string GetVirtualHost()
        {
            if (string.IsNullOrWhiteSpace(_virtualHost))
                return "/";
            return _virtualHost;
        }

        public KebabCaseEndpointNameFormatter GetKebabCaseEndpointNameFormatter()
        {
            if (string.IsNullOrWhiteSpace(Prefix))
                return null;
            return new KebabCaseEndpointNameFormatter($"{Prefix}:", false);
        }
    }
}
