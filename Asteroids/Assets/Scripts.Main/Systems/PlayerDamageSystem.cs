
using Scripts.CommonExtensions;
using Scripts.Main.Components;

namespace Scripts.Main.Systems
{
    // public class PlayerDamageSystem : SystemBase
    // {
    //     public override void Run()
    //     {
    //         base.Run();
    //
    //         var playerEntities = _world.GetEntity<PlayerDamageComponent>();
    //
    //         for (int i = 0; i < playerEntities.Length; i++)
    //         {
    //             var playerEntity = playerEntities[i];
    //             playerEntity.RemoveComponent<PlayerDamageComponent>();
    //             if (playerEntity.GetComponent<GameObjectComponent>() is { } gameObjectComponent)
    //                 gameObjectComponent.GameObject.SetActiveOptimized(false);
    //             playerEntity.AddComponent(new PLayerDiedComponent());
    //         }
    //     }
    // }
}