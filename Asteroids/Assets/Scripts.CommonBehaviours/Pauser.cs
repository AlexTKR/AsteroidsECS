using Zenject;

namespace Scripts.CommonBehaviours
{
    public class Pauser : IPauseBehaviour
    {
        private IPauseBehaviour[] _pauseBehaviours;

        [Inject]
        public void Construct(IPauseBehaviour[] pauseBehaviours)
        {
            _pauseBehaviours = pauseBehaviours;
        }

        public void SetPausedStatus(bool status)
        {
            IPauseBehaviour.IsPaused = status;
            
            for (int i = 0; i < _pauseBehaviours?.Length; i++)
            {
                _pauseBehaviours[i].SetPausedStatus(status);
            }
        }
    }
}