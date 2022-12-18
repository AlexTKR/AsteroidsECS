using Leopotam.Ecs;
using Scripts.CommonBehaviours;
using Scripts.Main.Components;

namespace Scripts.Main.Systems
{
    public class GameOverSystem : IEcsRunSystem
    {
        private IPauseBehaviour _pauseBehaviour;
        private EcsFilter<PlayerComponent, DiedComponent> _playerDiedFilter;

        public void Run()
        {
            if (_playerDiedFilter.IsEmpty())
                return;

            _pauseBehaviour.SetPausedStatus(true);
        }
    }
}