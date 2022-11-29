using Controllers;
using ECS.Systems;
using Scripts.ECS.System;
using Scripts.ECS.World;
using Scripts.Main.Entities;
using UnityEngine;

namespace Scripts.Main.Systems
{
    public class PlayerSpawnSystem : SystemBase
    {
        public override void Init(WorldBase world)
        {
            base.Init(world);
            var playerPrefab = _world.GetBehavior<ILoadPlayer>().LoadPLayer().Load(runAsync: false).Result;
            var player = Object.Instantiate<PlayerMonoEntity>(playerPrefab, Vector3.zero, Quaternion.identity);
            world.AddEntity(player.Convert(_world.GetNewEntity()));
            world.AddEntity(player.LaserMonoEntity.Convert(_world.GetNewEntity()));
        }
    }
}
