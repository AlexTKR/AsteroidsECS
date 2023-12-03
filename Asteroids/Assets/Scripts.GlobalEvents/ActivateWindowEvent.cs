using Scripts.Common;
using UnityEngine;

namespace Scripts.GlobalEvents
{
    public class ActivateWindowEvent : EventBase<ActivateWindowEvent>
    {
        public bool ShowOnTop = false;
        public bool TrackWindow = true;
        public WindowId WindowId;

        public override void UnPublish()
        {
            base.UnPublish();
            ShowOnTop = false;
            TrackWindow = true;
            WindowId = WindowId.None;
        }
    }
}
