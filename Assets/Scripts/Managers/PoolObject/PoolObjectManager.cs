using Assets.Scripts.Data;
using Assets.Scripts.Managers.PoolObject;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjectManager : Singleton<PoolObjectManager>
{
    public int amountOfProjectiles;

    public Projectile bulletPrefab;
    public Projectile grenadPrefab;
    public Projectile tennisBallPrefab;

    private Dictionary<ProjectileType, List<IPoolable>> pools;

    private void Awake()
    {
        pools = new Dictionary<ProjectileType, List<IPoolable>>();

        var tennisBalls = new List<IPoolable>();
        var bullets = new List<IPoolable>();
        var grenades = new List<IPoolable>();

        for (int i = 0; i < amountOfProjectiles; i++)
        {
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            var grenad = Instantiate(grenadPrefab, transform.position, Quaternion.identity);
            var tennisBall = Instantiate(tennisBallPrefab, transform.position, Quaternion.identity);

            bullet.transform.SetParent(transform);
            grenad.transform.SetParent(transform);
            tennisBall.transform.SetParent(transform);

            bullet.gameObject.SetActive(false);
            grenad.gameObject.SetActive(false);
            tennisBall.gameObject.SetActive(false);

            bullets.Add(bullet);
            grenades.Add(grenad);
            tennisBalls.Add(tennisBall);
        }

        pools.Add(ProjectileType.TennisBall, tennisBalls);
        pools.Add(ProjectileType.Bullet, bullets);
        pools.Add(ProjectileType.Grenade, grenades);

    }

    public IPoolable GetPooledObject(ProjectileType projectileType)
    {
        IPoolable obj;
        Projectile newObj = null;

        if (pools[projectileType].Count > 0)
        {
            obj = pools[projectileType][0];
            pools[projectileType].Remove(obj);
        }
        else
        {
            switch (projectileType)
            {
                case ProjectileType.Grenade:
                    newObj = Instantiate(grenadPrefab, transform.position, Quaternion.identity);
                    break;
                case ProjectileType.TennisBall:
                    newObj = Instantiate(tennisBallPrefab, transform.position, Quaternion.identity);
                    break;
                case ProjectileType.Bullet:
                    newObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    break;
            }

            newObj.transform.SetParent(transform);
            newObj.gameObject.SetActive(false);

            obj = newObj;
        }

        obj.onReleseEvent += Obj_onReleseEvent;

        return obj;
    }

    private void Obj_onReleseEvent(IPoolable obj)
    {
        obj.Reset();
        objectToReturn.transform.SetParent(transform);
        pools[objectToReturn.projectileType].Add(objectToReturn);
    }

    public void ReturnToPool(Projectile objectToReturn)
    {
        objectToReturn.transform.SetParent(transform);
        pools[objectToReturn.projectileType].Add(objectToReturn);
    }
}
