using System.Collections.Generic;
using Leopotam.Ecs;
using Zenject;

namespace Scripts.Common
{
    public interface IProcessTick : IRegisterTickSystems
    {
        void Tick();
        void FixedTick();
    }

    public interface IRegisterTickSystems
    {
        void RegisterRunSystem(IEcsRunSystem runSystem);
        void RegisterPhysicsRunSystem(IEcsRunSystem runSystem);
    }

    public class TickProcessor : IProcessTick 
    {
        private List<IEcsRunSystem> _runSystems = new List<IEcsRunSystem>();
        private List<IEcsRunSystem> _physicsRunSystems = new List<IEcsRunSystem>();
 
        private bool _runSystemsPaused;
        private bool _physicsRunSystemsPaused;
        
        public void Tick()
        {
            TickSystems();
        }

        public void FixedTick()
        {
            TickPhysicsSystems();
        }

        public void RegisterRunSystem(IEcsRunSystem runSystem)
        {
            _runSystems.Add(runSystem);
        }

        public void RegisterPhysicsRunSystem(IEcsRunSystem runSystem)
        {
            _physicsRunSystems.Add(runSystem);
        }

        private void TickSystems()
        {
            for (int i = 0; i < _runSystems.Count; i++)
            {
                _runSystems[i].Run();
            }
        }

        private void TickPhysicsSystems()
        {
            for (int i = 0; i < _physicsRunSystems.Count; i++)
            {
                _physicsRunSystems[i].Run();
            }
        }
    }
}