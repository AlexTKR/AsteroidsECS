using Leopotam.Ecs;
using Scripts.Main.Components;
using UnityEngine;

namespace Scripts.Main.Converters
{
    public class ScoreEntityConverter : MonoConverterBase
    {
        [SerializeField] private int _scoreForEntity;

        public override void Convert(ref EcsEntity entity)
        {
            entity.Get<ScoreEntityComponent>() = new ScoreEntityComponent()
            {
                ScoreForEntity = _scoreForEntity
            };
        }
    }
}