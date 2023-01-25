using System;

namespace Scripts.GlobalEvents
{
    public abstract class EventBase
    {
        private bool _isPublished;

        public Action OnPublish;

        public bool IsPublished
        {
            get => _isPublished;
            set
            {
                _isPublished = value;
                if (value)
                    OnPublish?.Invoke();
            }
        }

        public void Publish()
        {
            IsPublished = true;
        }

        public void UnPublish()
        {
            IsPublished = false;
        }
    }
}