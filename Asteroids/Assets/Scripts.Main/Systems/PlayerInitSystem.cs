using Controllers;
using Leopotam.Ecs;
using Scripts.Main.Components;
using UnityEngine;

namespace Scripts.Main.Systems
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private ILoadPlayer _loadPlayer;

        public void Init()
        {
            var playerPrefab = _loadPlayer.LoadPLayer().Load(runAsync: false).Result;
            var playerSpawnEntity = _world.NewEntity();
            playerSpawnEntity.Get<SpawnComponent>() = new SpawnComponent()
            {
                Prefab = playerPrefab.gameObject,
                Position = Vector3.zero,
                Rotation = Quaternion.identity,
                Parent = null,
                IsActive = true
            };
        }
    }
}