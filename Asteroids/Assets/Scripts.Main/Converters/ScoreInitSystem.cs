using Leopotam.Ecs;
using Scripts.Main.Components;

namespace Scripts.Main.Converters
{
    public class ScoreInitSystem : IEcsInitSystem
    {
        private EcsWorld _ecsWorld;
        
        public void Init()
        {
            _ecsWorld.NewEntity().Get<GameScoreComponent>();
        }
    }
}
