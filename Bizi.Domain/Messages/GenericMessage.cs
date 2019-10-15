using System;

namespace Bizi.Domain
{
    /// <summary>
    /// Base class for any Event Message, holding reference to the sender and content
    /// </summary>
    /// <typeparam name="TContent"></typeparam>
    public class GenericMessage<TContent>
    {
        public TContent Content { get; protected set; }
        public WeakReference sender { get; protected set; }

        public GenericMessage(object _sender, TContent _content)
        {
            sender = new WeakReference(_sender);
            Content = _content;
        }
    }
}
