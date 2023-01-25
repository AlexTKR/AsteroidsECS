using System.Threading.Tasks;
using Scripts.Main.Entities;
using Scripts.Main.Settings;
using Scripts.UI.Canvas;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public interface ILoadable<T>
{
    Task<T> Load(bool autoRelease = true, bool runAsync = true);
    void Release();
}

public class LoadReference<T, TIn> : ILoadable<T>
{
    private string _id;
    private AsyncOperationHandle _handle;

    public LoadReference(string id)
    {
        _id = id;
    }

    public async Task<T> Load(bool autoRelease = true, bool runAsync = true)
    {
        _handle = Addressables.LoadAssetAsync<TIn>(_id);

        if (runAsync)
            await _handle.Task;
        else
            _handle.WaitForCompletion();

        var result = typeof(TIn) == typeof(GameObject)
            ? ((GameObject)_handle.Result).GetComponent<T>()
            : (T)_handle.Result;

        if (autoRelease)
        {
            Release();
        }

        return result;
    }

    public void Release()
    {
        Addressables.Release(_handle);
    }
}

namespace Controllers
{
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

    public interface ILoadGameSettings
    {
        ILoadable<GameSettings> LoadGameSettings();
    }

    public interface ILoadCanvas<T>
    {
        ILoadable<T> LoadCanvas();
    }

    public class BundleController : ILoadPlayer, ILoadBullet,
        ILoadAsteroids, ILoadUfo, ILoadGameSettings, ILoadCanvas<MainCanvas>
    {
        private ILoadable<PlayerMonoEntity> _playerMonoEntity;
        private ILoadable<BulletMonoEntity> _bulletMonoEntity;
        private ILoadable<BigAsteroidMonoEntity> _bigAsteroidsMonoEntity;
        private ILoadable<SmallAsteroidMonoEntity> _smallAsteroidsMonoEntity;
        private ILoadable<UfoMonoEntity> _ufoMonoEntity;
        private ILoadable<GameSettings> _loadGameSettings;
        private ILoadable<MainCanvas> _mainCanvas;

        public ILoadable<BigAsteroidMonoEntity> LoadBigAsteroid()
        {
            return _bigAsteroidsMonoEntity ??= new LoadReference<BigAsteroidMonoEntity, GameObject>("Asteriod_big");
        }

        public ILoadable<SmallAsteroidMonoEntity> LoadSmallAsteroid()
        {
            return _smallAsteroidsMonoEntity ??=
                new LoadReference<SmallAsteroidMonoEntity, GameObject>("Asteriod_small");
        }

        public ILoadable<PlayerMonoEntity> LoadPLayer()
        {
            return _playerMonoEntity ??= new LoadReference<PlayerMonoEntity, GameObject>("Player");
        }

        public ILoadable<BulletMonoEntity> LoadBullet()
        {
            return _bulletMonoEntity ??= new LoadReference<BulletMonoEntity, GameObject>("Bullet");
        }

        public ILoadable<UfoMonoEntity> LoadUfo()
        {
            return _ufoMonoEntity ??= new LoadReference<UfoMonoEntity, GameObject>("Ufo");
        }

        public ILoadable<GameSettings> LoadGameSettings()
        {
            return _loadGameSettings ??= new LoadReference<GameSettings, GameSettings>("GameSettings");
        }

        public ILoadable<MainCanvas> LoadCanvas()
        {
            return _mainCanvas ??= new LoadReference<MainCanvas, GameObject>("MainCanvas");
        }
    }
}