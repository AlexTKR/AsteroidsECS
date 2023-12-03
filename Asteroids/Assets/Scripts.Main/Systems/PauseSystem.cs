using Leopotam.Ecs;
using Scripts.Main.Components;

namespace Scripts.Main.Systems
{
    public class PauseSystem : IEcsInitSystem
    {
        private EcsWorld _ecsWorld;
        
        public void Init()
        {
            _ecsWorld.NewEntity().Get<SystemPausedComponent>().SystemPauseActive = false;
        }
    }
}
