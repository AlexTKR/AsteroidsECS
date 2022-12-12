using System;
using Scripts.Main.Components;
using Scripts.ViewViewModelBehavior;

namespace Scripts.Main.Systems
{
    // public class UiSystem : SystemBase
    // {
    //     private IMainHubBehavior _mainHubBehavior;
    //
    //     public override void Init(WorldBase world)
    //     {
    //         base.Init(world);
    //         _mainHubBehavior = _world.GetBehavior<IMainHubBehavior>();
    //     }
    //
    //     public override void Run()
    //     {
    //         base.Run();
    //
    //         var playerEntities = _world.GetEntity<PlayerComponent>();
    //         var laserEntities = _world.GetEntity<LaserComponent>();
    //
    //         for (int i = 0; i < playerEntities.Length; i++)
    //         {
    //             var playerEntity = playerEntities[i];
    //             var transform = playerEntity.GetComponent<TransformComponent>()?.Transform;
    //             var playerComponent = playerEntity.GetComponent<PlayerComponent>();
    //             if (transform is { })
    //             {
    //                 _mainHubBehavior.PlayerCoordinates.Value = transform.position;
    //                 _mainHubBehavior.PlayerTurnAngle.Value = transform.rotation.eulerAngles.z;
    //             }
    //
    //             _mainHubBehavior.PlayerInstantSpeed.Value = playerComponent.Acceleration;
    //         }
    //
    //         for (int i = 0; i < laserEntities.Length; i++)
    //         {
    //             var laserEntity = laserEntities[i];
    //             var laserComponent = laserEntity.GetComponent<LaserComponent>();
    //             var delayLaserComponent = laserEntity.GetComponent<DelayLaserComponent>();
    //
    //             if (laserComponent is { })
    //                 _mainHubBehavior.LaserCount.Value = laserComponent.LaserCount;
    //
    //             if (delayLaserComponent is { })
    //                 _mainHubBehavior.LaserDelay.Value = (delayLaserComponent.DelayTimer - DateTime.Now.TimeOfDay).Seconds;
    //
    //         }
    //     }
    // }
}