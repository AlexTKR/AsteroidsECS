using System;
using UnityEngine;

namespace Scripts.Main.Composition
{
    public interface IPauseBehaviour 
    {
        void Pause(bool status);
    }
    
    public class Pauser : IPauseBehaviour
    {
        private IPauseBehaviour[] _pauseBehaviours;
        
        public Pauser( IPauseBehaviour[] pauseBehaviours)
        {
            _pauseBehaviours = pauseBehaviours;
        }
        
        public void Pause(bool status)
        {
            for (int i = 0; i < _pauseBehaviours.Length; i++)
            {
                _pauseBehaviours[i].Pause(status);
            }
        }
    }
}
