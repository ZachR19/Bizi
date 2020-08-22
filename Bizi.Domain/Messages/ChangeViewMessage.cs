using Bizi.UI.Interfaces;

namespace Bizi.Domain.Messages
{
    public class ChangeViewMessage : GenericMessage<IBiziViewModel>
    {
        public ChangeViewMessage(object _sender, IBiziViewModel _content) : base(_sender, _content)
        {

        }
    }
}
