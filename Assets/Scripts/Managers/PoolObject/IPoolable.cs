using System;

namespace Assets.Scripts.Managers.PoolObject
{
    public interface IPoolable
    {
        event Action<IPoolable> onReleseEvent;

        public void Init(PoolInitData data);

        public void Reset();

        public void Relese();
    }
}
