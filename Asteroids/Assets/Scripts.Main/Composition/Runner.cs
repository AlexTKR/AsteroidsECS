using System;

namespace Scripts.Main.Composition
{
    public interface IRun : ISetOnActionBehaviour
    {
        void Run();
    }

    public interface ISetOnActionBehaviour
    {
        void SetAction(Action action);
    }

    public interface IFixedRun : ISetOnActionBehaviour
    {
        void FixedRun();
    }
    
    public class Runner : IRun, IPauseBehaviour
    {
        private bool isPaused;
        private Action _onRun;
        
        public void Run()
        {
            if(isPaused)
                return;
            
            _onRun?.Invoke();
        }

    

        public void Pause(bool status)
        {
            isPaused = status;
        }

        public void SetAction(Action action)
        {
            _onRun += action;
        }
    }

    public class FixedRunner : IFixedRun, IPauseBehaviour
    {
        private bool isPaused;
        private Action _onFixedRun;
        
        public void FixedRun()
        {
            if(isPaused)
                return;
            
            _onFixedRun?.Invoke();
        }
        
        public void Pause(bool status)
        {
            isPaused = status;
        }

        public void SetAction(Action action)
        {
            _onFixedRun = action;
        }
    }
}
