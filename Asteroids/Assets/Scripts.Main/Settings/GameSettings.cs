using UnityEngine;

namespace Scripts.Main.Settings
{
    [CreateAssetMenu(menuName = "GameSettings")]
    public class GameSettings :ScriptableObject
    {
        public int MaxSmallAsteroidsSpawnCount;
        public Vector2 SmallAsteroidsSpawnCountDegrees;

        public int LaserCount;
        public int LaserDelay;

        public float AccelerationFadeValue;
        public int playerRotationSpeed;
    }
}
