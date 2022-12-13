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
        public int LaserActiveTime;

        public float AsteroidsSpawnDelay;
        public float UfoSpawnDelay;
    }
}
