using Assets.Scripts.Managers.PoolObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Projectiles
{
    public class PoolInitProjectileData : PoolInitData
    {
        public Transform IniPosition { get; set; } 

        public PoolInitProjectileData(Transform transform) 
        {
            IniPosition = transform;
        }
    }
}
