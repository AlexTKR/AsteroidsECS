using Scripts.Main.Components;

namespace Scripts.Main.Factories
{
    public interface IFactory<TOut, TIn> where TIn: struct
    {
        void Get(ref TOut entity ,ref TIn inData);
    }
}
