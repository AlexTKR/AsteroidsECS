using Leopotam.Ecs;
using Scripts.CommonBehaviours;
using Scripts.Main.Components;

namespace Scripts.Main.Systems
{
    public class GameOverSystem : IEcsRunSystem
    {
        private IPauseBehaviour _pauseBehaviour;
        private EcsFilter<PlayerComponent, DiedComponent, PlayerDataComponent> _playerDiedFilter;
        private EcsFilter<GameScoreComponent> _scoreFilter;

        public void Run()
        {
            if (_playerDiedFilter.IsEmpty() || IPauseBehaviour.IsPaused)
                return;

            ref var playerDataComponent = ref _playerDiedFilter.Get3(0);
            ref var scoreComponent = ref _scoreFilter.Get1(0);
            playerDataComponent.PlayerData.Score.Value = scoreComponent.Score;
            _pauseBehaviour.SetPausedStatus(true);
        }
    }
}