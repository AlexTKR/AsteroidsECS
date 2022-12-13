using System;

namespace Scripts.CommonBehaviours
{
    public interface IRunner
    {
        void SetRunCallBack(Action callBack);
        void Run();
    }
}
