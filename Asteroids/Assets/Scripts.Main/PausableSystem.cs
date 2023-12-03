using Leopotam.Ecs;
using Scripts.Main.Components;

namespace Scripts.Main
{
    public abstract class PausableSystem : IEcsRunSystem
    {
        protected EcsFilter<SystemPausedComponent> _systemPausedFilter;

        public void Run()
        {
            ref SystemPausedComponent pausedComponent = ref _systemPausedFilter.Get1(0);
            
            if (pausedComponent.SystemPauseActive)
                return;

            Tick();
        }

        protected virtual void Tick()
        {
        }
    }
}