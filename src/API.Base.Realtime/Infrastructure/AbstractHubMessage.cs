namespace API.Base.Realtime.Infrastructure
{
    public class AbstractHubMessage<TMessage, TConncetionKey>
    {
        public TConncetionKey From { get; set; }
        public TConncetionKey To { get; set; }
        public TMessage Message { get; set; }
    }
}