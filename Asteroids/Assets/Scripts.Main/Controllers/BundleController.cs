using System;
using Scripts.Main.Entities;
using Scripts.Main.Loadable;
using Scripts.Main.Settings;
using Scripts.UI.Canvas;
using UnityEngine;

namespace Scripts.Main.Controllers
{
    #region GameEntities

    public interface ILoadGameEntities : ILoadPlayer,ILoadBullet
        ,ILoadAsteroids, ILoadUfo
    {
        
    }
    
    public interface ILoadPlayer
    {
        ILoadable<PlayerMonoEntity> LoadPLayer();
    }

    public interface ILoadBullet
    {
        ILoadable<BulletMonoEntity> LoadBullet();
    }

    public interface ILoadAsteroids
    {
        ILoadable<BigAsteroidMonoEntity> LoadBigAsteroid();
        ILoadable<SmallAsteroidMonoEntity> LoadSmallAsteroid();
    }

    public interface ILoadUfo
    {
        ILoadable<UfoMonoEntity> LoadUfo();
    }

    #endregion

    #region GameSettings

    public interface ILoadGameSettings
    {
        ILoadable<GameSettings> LoadGameSettings();
    }

    #endregion

    #region View

    public interface ILoadView : ILoadCanvas<MainCanvas>
    {
        
    }

    public interface ILoadCanvas<T>
    {
        ILoadable<T> LoadCanvas(Type type);
    }

    #endregion

    public class BundleController : ILoadGameEntities, ILoadGameSettings, ILoadView
    {
        private ILoadable<PlayerMonoEntity> _playerMonoEntity;
        private ILoadable<BulletMonoEntity> _bulletMonoEntity;
        private ILoadable<BigAsteroidMonoEntity> _bigAsteroidsMonoEntity;
        private ILoadable<SmallAsteroidMonoEntity> _smallAsteroidsMonoEntity;
        private ILoadable<UfoMonoEntity> _ufoMonoEntity;
        private ILoadable<GameSettings> _loadGameSettings;
        private ILoadable<MainCanvas> _mainCanvas;

        #region LoadableIds
        
        private const string AsteroidBigId = "Asteriod_big";
        private const string AsteroidSmallId = "Asteriod_small";
        private const string PlayerId = "Player";
        private const string BulletId = "Bullet";
        private const string UfoId = "Ufo";
        private const string GameSettingsId = "GameSettings";
        private const string MainCanvasId = "MainCanvas";
        
        #endregion

        public ILoadable<BigAsteroidMonoEntity> LoadBigAsteroid()
        {
            return _bigAsteroidsMonoEntity ??= new LoadReference<BigAsteroidMonoEntity, GameObject>(AsteroidBigId);
        }

        public ILoadable<SmallAsteroidMonoEntity> LoadSmallAsteroid()
        {
            return _smallAsteroidsMonoEntity ??= new LoadReference<SmallAsteroidMonoEntity, GameObject>(AsteroidSmallId);
        }

        public ILoadable<PlayerMonoEntity> LoadPLayer()
        {
            return _playerMonoEntity ??= new LoadReference<PlayerMonoEntity, GameObject>(PlayerId);
        }

        public ILoadable<BulletMonoEntity> LoadBullet()
        {
            return _bulletMonoEntity ??= new LoadReference<BulletMonoEntity, GameObject>(BulletId);
        }

        public ILoadable<UfoMonoEntity> LoadUfo()
        {
            return _ufoMonoEntity ??= new LoadReference<UfoMonoEntity, GameObject>(UfoId);
        }

        public ILoadable<GameSettings> LoadGameSettings()
        {
            return _loadGameSettings ??= new LoadReference<GameSettings, GameSettings>(GameSettingsId);
        }

        public ILoadable<MainCanvas> LoadCanvas(Type type)
        {
            if (type == typeof(MainCanvas))
                return _mainCanvas ??= new LoadReference<MainCanvas, GameObject>(MainCanvasId);

            return default;
        }
    }
}