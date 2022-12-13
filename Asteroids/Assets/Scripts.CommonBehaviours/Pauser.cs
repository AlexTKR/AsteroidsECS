using Zenject;

namespace Scripts.CommonBehaviours
{
    public class Pauser : IPauseBehaviour
    {
        private IPauseBehaviour[] _pauseBehaviours;
        
        public Pauser(IPauseBehaviour[] pauseBehaviours)
        {
            _pauseBehaviours = pauseBehaviours;
        }

        public void SetPausedStatus(bool status)
        {
            if(_pauseBehaviours is null)
                return;
            
            for (int i = 0; i < _pauseBehaviours.Length; i++)
            {
                _pauseBehaviours[i].SetPausedStatus(status);
            }
        }
    }
}
