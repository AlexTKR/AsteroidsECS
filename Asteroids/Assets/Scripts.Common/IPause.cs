using System.Collections.Generic;

namespace Scripts.Common
{
    public interface IPauseRunSystems
    {
        void Pause(bool status);
    }

    public interface IPausePhysicsRunSystems
    {
        void Pause(bool status);
    }

    public interface IPauseSystems
    {
        void RegisterPauseSystem(IPauseRunSystems pauseRunSystem);
        void RegisterPausePhysicsSystem(IPausePhysicsRunSystems pausePhysicsRunSystem);
        
        void PauseRunSystems(bool status);
        void PausePhysicsRunSystems(bool status);
    }

    public class PauseProcessor : IPauseSystems
    {
        private List<IPauseRunSystems> _pauseRunSystems = new List<IPauseRunSystems>();
        private readonly List<IPausePhysicsRunSystems> _pausePhysicsRunSystems = new List<IPausePhysicsRunSystems>();

        public void RegisterPauseSystem(IPauseRunSystems pauseRunSystem)
        {
            _pauseRunSystems.Add(pauseRunSystem);
        }

        public void RegisterPausePhysicsSystem(IPausePhysicsRunSystems pausePhysicsRunSystem)
        {
            _pausePhysicsRunSystems.Add(pausePhysicsRunSystem);
        }

        public void PauseRunSystems(bool status)
        {
            for (int i = 0; i <  _pauseRunSystems.Count; i++)
            {
                _pauseRunSystems[i].Pause(status);
            }
        }

        public void PausePhysicsRunSystems(bool status)
        {
            for (int i = 0; i <   _pausePhysicsRunSystems.Count; i++)
            {
                _pausePhysicsRunSystems[i].Pause(status);
            }
        }
    }
}
