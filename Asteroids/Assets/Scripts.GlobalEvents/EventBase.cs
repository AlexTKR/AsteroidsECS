using System;

namespace Scripts.GlobalEvents
{
    public abstract class EventBase<T> where T : class
    {
        private bool _isPublished;

        public Action<T> OnPublish;

        public bool IsPublished
        {
            get => _isPublished;
            set
            {
                _isPublished = value;
                if (value)
                    OnPublish?.Invoke(this as T);
            }
        }

        public virtual void Publish()
        {
            IsPublished = true;
        }

        public virtual void UnPublish()
        {
            IsPublished = false;
        }
    }
}