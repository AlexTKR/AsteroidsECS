using Scripts.Main.Components;

namespace Scripts.Main.Factories
{
    public interface IFactory<TOut, TIn>
    {
        void Get(ref TOut entity ,ref TIn inData);
    }
}
