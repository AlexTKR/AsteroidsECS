using Leopotam.Ecs;
using Scripts.Main.Components;
using UnityEngine;

namespace Scripts.Main.Converters
{
    public abstract class MonoConverterBase : MonoBehaviour
    {
        public abstract void Convert(ref EcsEntity entity);
    }
}