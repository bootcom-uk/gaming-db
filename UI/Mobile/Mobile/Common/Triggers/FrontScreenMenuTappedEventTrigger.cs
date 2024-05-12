namespace Mobile.Common.Triggers
{
    public class FrontScreenMenuTappedEventTrigger : TriggerAction<Frame>
    {
        protected override void Invoke(Frame sender)
        {
            sender.StyleClass = ["TappedFrontScreenTile"];
        }
    }
}
