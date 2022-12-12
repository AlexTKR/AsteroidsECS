using Scripts.Main.Components;
using Scripts.Main.Composition;
using Scripts.ViewViewModelBehavior;

namespace Scripts.Main.Systems
{
    // public class GameOverSystem : SystemBase
    // {
    //     IPauseBehaviour _pauseBehaviour;
    //     private IGameOverPanelBehaviour _gameOverPanelBehaviour;
    //     
    //     public override void Init(WorldBase world)
    //     {
    //         base.Init(world);
    //         _pauseBehaviour = world.GetBehavior<IPauseBehaviour>();
    //         _gameOverPanelBehaviour = _world.GetBehavior<IGameOverPanelBehaviour>();
    //     }
    //
    //     public override void Run()
    //     {
    //         base.Run();
    //         var playerDiedEntities = _world.GetEntity<PLayerDiedComponent>();
    //
    //         for (int i = 0; i < playerDiedEntities.Length; i++)
    //         {
    //             var playerDiedEntity = playerDiedEntities[i];
    //             playerDiedEntity.RemoveComponent<PLayerDiedComponent>();
    //             if (playerDiedEntity.GetComponent<GameScoreComponent>() is { } gameScoreComponent)
    //                 _gameOverPanelBehaviour.Score.Value = gameScoreComponent.Score;
    //
    //             _pauseBehaviour.Pause(true);
    //             _gameOverPanelBehaviour.OnGameOver?.Invoke();
    //         }
    //     }
    // }
}
