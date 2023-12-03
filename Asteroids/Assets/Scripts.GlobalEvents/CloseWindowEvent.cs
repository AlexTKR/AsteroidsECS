using Scripts.Common;

namespace Scripts.GlobalEvents
{
    public class CloseWindowEvent : EventBase<CloseWindowEvent>
    {
        public WindowId WindowId;
    }
}
