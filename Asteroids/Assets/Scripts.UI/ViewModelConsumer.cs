using Scripts.Common;

namespace Scripts.UI
{
    public abstract class ViewModelConsumer : UIPanel
    {
        public abstract void Init(ByTypeProvider viewModelProvider);
    }
}