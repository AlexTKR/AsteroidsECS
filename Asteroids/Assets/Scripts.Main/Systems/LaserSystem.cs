using System;
using System.Threading.Tasks;
using Scripts.CommonExtensions;
using Scripts.Main.Components;
using Scripts.Main.Settings;

namespace Scripts.Main.Systems
{
    // public class LaserSystem : SystemBase
    // {
    //     public override void Run()
    //     {
    //         base.Run();
    //
    //         var laserEntities = _world.GetEntity<LaserComponent>();
    //
    //         for (int i = 0; i < laserEntities.Length; i++)
    //         {
    //             var currLaserEntity = laserEntities[i];
    //             var gameObjectComponent = currLaserEntity.GetComponent<GameObjectComponent>();
    //             var laserComponent = currLaserEntity.GetComponent<LaserComponent>();
    //             var delayLaserComponent = currLaserEntity.GetComponent<DelayLaserComponent>();
    //             if (gameObjectComponent is null || laserComponent is null)
    //                 continue;
    //
    //             if (currLaserEntity.GetComponent<ActiveLaserComponent>() is { } activeLaserComponent)
    //             {
    //                 if (DateTime.Now.TimeOfDay >= activeLaserComponent.ActiveTimer)
    //                 {
    //                     currLaserEntity.RemoveComponent<ActiveLaserComponent>();
    //                     gameObjectComponent.GameObject.SetActiveOptimized(false);
    //                 }
    //
    //                 continue;
    //             }
    //
    //             if (delayLaserComponent is { })
    //             {
    //                 if (DateTime.Now.TimeOfDay >= delayLaserComponent.DelayTimer)
    //                 {
    //                     currLaserEntity.RemoveComponent<DelayLaserComponent>();
    //                     laserComponent.LaserCount = RuntimeSharedData.GameSettings.LaserCount;
    //                 }
    //             }
    //
    //             var shootComponent = currLaserEntity.GetComponent<ShootLaserComponent>(true);
    //             if (shootComponent is null)
    //                 continue;
    //
    //             if (--laserComponent.LaserCount <= 0)
    //                 currLaserEntity.AddComponent(new DelayLaserComponent()
    //                     { DelayTimer = DateTime.Now.TimeOfDay + TimeSpan.FromSeconds(20f) });
    //
    //             gameObjectComponent.GameObject.SetActiveOptimized(true);
    //             currLaserEntity.AddComponent(new ActiveLaserComponent()
    //                 { ActiveTimer = DateTime.Now.TimeOfDay + TimeSpan.FromSeconds(1f) });
    //         }
    //     }
    //
    //     private async void DisableLaserAfterDelay(GameObjectComponent gameObjectComponent)
    //     {
    //         await Task.Delay(TimeSpan.FromSeconds(1f));
    //         gameObjectComponent.GameObject.SetActiveOptimized(false);
    //     }
    // }
}