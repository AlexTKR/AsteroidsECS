using System.Collections.Generic;
using Scripts.CommonExtensions;
using Scripts.ECS.World;

namespace Scripts.ECS.System
{
    public abstract class SystemsBase
    {
        protected List<SystemBase> _systems
            = new List<SystemBase>();

        protected WorldBase _world;

        protected SystemsBase(WorldBase world)
        {
            _world = world;
        }

        public virtual void Run()
        {
            for (int i = 0; i < _systems.Count; i++)
            {
                _systems[i].Run();
            }
        }

        public virtual void PreInit()
        {
            for (int i = 0; i < _systems.Count; i++)
            {
                _systems[i].PreInit();
            }
        }

        public virtual void Init()
        {
            for (int i = 0; i < _systems.Count; i++)
            {
                _systems[i].Init(_world);
            }
            
            RemoveOneFrames();
        }

        public virtual void Destroy()
        {
            for (int i = 0; i < _systems.Count; i++)
            {
                _systems[i].Destroy();
            }

            _systems = null;
        }
        
        public void AddSystem(SystemBase system)
        {
            _systems.Add(system);
        }

        private void RemoveOneFrames()
        {
            for (int i = _systems.Count - 1; i >= 0; i--)
            {
                var system = _systems[i];
                if(!system.OneFrame)
                    continue;

                var removeSystem = _systems.RemoveAtAndReturn(i);
                removeSystem.Destroy();
            }
            
            _systems.TrimExcess();
        }
    }

    public static partial class Extensions
    {
        public static SystemsBase Add(this SystemsBase systems, SystemBase system)
        {
            systems.AddSystem(system);
            return systems;
        }
        
        public static SystemsBase Initialize(this SystemsBase systems)
        {
            systems.Init();
            return systems;
        }
    }

    public class RunSystems : SystemsBase
    {
        public RunSystems(WorldBase world) : base(world)
        {
        }
    }
}
