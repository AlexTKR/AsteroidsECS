using Leopotam.Ecs;
using Scripts.Common;
using Scripts.Main.Components;

namespace Scripts.Main.Systems
{
    public class GameOverSystem : IEcsRunSystem
    {
        private IPauseSystems _pauseSystems;
        private EcsFilter<PlayerComponent, DiedComponent, PlayerDataComponent> _playerDiedFilter;
        private EcsFilter<GameScoreComponent> _scoreFilter;

        public void Run()
        {
            if (_playerDiedFilter.IsEmpty()) 
                return;

            ref var playerDataComponent = ref _playerDiedFilter.Get3(0);
            ref var scoreComponent = ref _scoreFilter.Get1(0);
            playerDataComponent.PlayerData.Score.Value = scoreComponent.Score;
            _pauseSystems.PauseRunSystems(true);
            _pauseSystems.PausePhysicsRunSystems(true);
        }
    }
}