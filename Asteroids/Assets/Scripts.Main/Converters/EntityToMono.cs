using Leopotam.Ecs;
using UnityEngine;

namespace Scripts.Main.Converters
{
    public class EntityToMono : MonoBehaviour
    {
        private MonoConverterBase[] _converters;

        public virtual void Convert(ref EcsEntity entity)
        {
            _converters ??= GetComponents<MonoConverterBase>();

            for (int i = 0; i < _converters.Length; i++)
            {
                _converters[i].Convert(ref entity);
            }
        }
    }
}