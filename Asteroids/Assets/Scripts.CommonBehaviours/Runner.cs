using System;

namespace Scripts.CommonBehaviours
{
    public class Runner : IRunner, IPauseBehaviour
    {
        private bool isPaused;
        private Action _runAction;
        
        public void SetRunCallBack(Action callBack)
        {
            _runAction = callBack;
        }

        public void Run()
        {
            if(isPaused)
                return;
            
            _runAction?.Invoke();
        }

        public void SetPausedStatus(bool status)
        {
            isPaused = status;
        }
    }
}
