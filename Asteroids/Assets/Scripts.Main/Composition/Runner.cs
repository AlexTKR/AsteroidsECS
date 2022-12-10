using System;

namespace Scripts.Main.Composition
{
    public interface IRun : IEventSetter
    {
        void Run();
    }

    public interface IEventSetter
    {
        void SetEvent(Action action);
    }

    public interface IFixedRun : IEventSetter
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

        public void SetEvent(Action action)
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

        public void SetEvent(Action action)
        {
            _onFixedRun = action;
        }
    }
}
