using Leopotam.Ecs;
using Scripts.CommonBehaviours;

namespace Scripts.Main.Systems
{
    public abstract class SystemBase : IEcsInitSystem, IEcsRunSystem
    {
        public virtual void Init()
        {
        }

        public virtual void Run()
        {
            if (IPauseBehaviour.IsPaused)
                return;
        }
    }
}