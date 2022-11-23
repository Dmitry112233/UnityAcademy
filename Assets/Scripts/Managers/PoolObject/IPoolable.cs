using System;
using UnityEngine;

namespace Assets.Scripts.Managers.PoolObject
{
    public interface IPoolable
    {
        event Action<IPoolable> onReleseEvent;

        public void Init(PoolInitData data);

        public void Reset(Transform parrentTransform);

        public void Relese();

        public void AfterCreate(Transform parrentTransform);
    }
}
