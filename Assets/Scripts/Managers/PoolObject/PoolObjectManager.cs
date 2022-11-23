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

    List<IPoolable> pool;

    private void Awake()
    {
        pool = new List<IPoolable>();

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

            pool.Add(bullet);
            pool.Add(grenad);
            pool.Add(tennisBall);
        }
    }

    public IPoolable GetPooledObject(IPoolable obj)
    {
        IPoolable newObj;

        newObj = pool.Find(x => x.GetType() == obj.GetType());

        if (newObj != null)
        {
            pool.Remove(newObj);
        }
        else
        {
            switch (obj.GetType().ToString())
            {
                case "Grenade":
                    newObj = Instantiate(grenadPrefab, transform.position, Quaternion.identity);
                    break;
                case "TennisBall":
                    newObj = Instantiate(tennisBallPrefab, transform.position, Quaternion.identity);
                    break;
                case "Bullet":
                    newObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    break;
            }

            newObj.AfterCreate(transform);
        }

        obj.onReleseEvent += Obj_onReleseEvent;

        return obj;
    }

    private void Obj_onReleseEvent(IPoolable obj)
    {
        obj.Reset(transform);
        pool.Add(obj);
    }
}
