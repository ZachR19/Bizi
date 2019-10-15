namespace Bizi.Domain
{
    public class LoadedMessage : GenericMessage<string>
    {
        public LoadedMessage(object sender, string _content) : base(sender, _content)
        {
        }
    }
}
