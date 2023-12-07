using System;
using Leopotam.Ecs;
using Scripts.Data;
using Scripts.Main.Components;
using Scripts.Main.Controllers;
using Scripts.Main.Entities;
using Scripts.Main.Loader;
using UnityEngine;

namespace Scripts.Main.Systems
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private ILoadPlayer _loadPlayer;
        private IDataProvider _dataProvider;

        public void Init()
        {
            var playerDataRepository = new PlayerDataRepository();
            _dataProvider.Add(playerDataRepository);
            PlayerMonoEntity playerPrefab = _loadPlayer.LoadPLayer().Load(runAsync: false).Result;
            var playerSpawnEntity = _world.NewEntity();
            playerSpawnEntity.Get<PlayerDataComponent>() = new PlayerDataComponent()
            {
                PlayerData = playerDataRepository.Data
            };
            
            var spawnComponent = new SpawnComponent()
            {
                Prefab = playerPrefab.gameObject,
                Position = Vector3.zero,
                Rotation = Quaternion.identity,
                Parent = null,
                IsActive = true,
                OnSpawn = () => { _loadPlayer.LoadPLayer().Release(); }
            };
            
            playerSpawnEntity.Get<SpawnComponent>() = spawnComponent;
        }

        private void OnPlayerSpawned(Action action)
        {
            throw new System.NotImplementedException();
        }
    }
}