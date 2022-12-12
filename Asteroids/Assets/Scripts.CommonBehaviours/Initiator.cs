using System.Threading.Tasks;
using Zenject;

namespace Scripts.CommonBehaviours
{
    public interface IInitiator :  IInit, IPreInit
    {
        
    }

    public class Initiator : IInitiator
    {
        private IInit[] _inits;
        private IPreInit[] _preInits;

        [Inject]
        private void Construct(IInit[] initControllers, IPreInit[] preInits)
        {
            _inits = initControllers;
            _preInits = preInits;
        }

        public void Init()
        {
            for (int i = 0; i < _inits.Length; i++)
            {
                 _inits[i].Init();
            }
        }

        public void PreInit()
        {
            for (int i = 0; i < _preInits.Length; i++)
            {
                _preInits[i].PreInit();
            }
        }
    }
}