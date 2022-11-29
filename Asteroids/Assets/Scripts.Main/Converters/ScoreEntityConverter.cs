using Scripts.ECS.Entity;
using Scripts.Main.Components;
using UnityEngine;

namespace Scripts.Main.Converters
{
    public class ScoreEntityConverter : MonoConverterBase
    {
        [SerializeField] private int _scoreForEntity;

        public override EntityBase Convert(EntityBase entity)
        {
            return entity.AddComponent(new ScoreEntityComponent() { ScoreForEntity = _scoreForEntity });
        }
    }
}