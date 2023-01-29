namespace Dictionary.Common.Events
{
    public class UserEmailChangedEvent
    {
        public string OldEmailAddress { get; set; }
        public string NewEmailAddress { get; set; }
    }
}
