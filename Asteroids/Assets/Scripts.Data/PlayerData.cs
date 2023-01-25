using Scripts.Reactive;

namespace Scripts.Data
{
    public sealed class PlayerData
    {
        public IReactiveValue<float> InstantSpeed = new ReactiveValue<float>();
        public IReactiveValue<int> Score = new ReactiveValue<int>();
        public IReactiveValue<int> LaserCount = new ReactiveValue<int>();
        public IReactiveValue<int> LaserDelay = new ReactiveValue<int>();
    }
}
