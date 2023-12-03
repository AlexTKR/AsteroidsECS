using Leopotam.Ecs;
using Scripts.Common;
using Scripts.Main.Components;

namespace Scripts.Main.Systems
{
    public class GameOverSystem : PausableSystem
    {
        private EcsFilter<PlayerComponent, DiedComponent, PlayerDataComponent> _playerDiedFilter;
        private EcsFilter<GameScoreComponent> _scoreFilter;

        protected override void Tick()
        {
            if (_playerDiedFilter.IsEmpty()) 
                return;

            ref PlayerDataComponent playerDataComponent = ref _playerDiedFilter.Get3(0);
            ref GameScoreComponent scoreComponent = ref _scoreFilter.Get1(0);
            playerDataComponent.PlayerData.Score.Value = scoreComponent.Score;
        }
    }
}