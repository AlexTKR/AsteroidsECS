namespace Scripts.CommonBehaviours
{
    public interface IPauseBehaviour
    {
        public static bool IsPaused;
        void SetPausedStatus(bool status);
    }
}
