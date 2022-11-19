using Assets.Scripts.Data;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjectManager : Singleton<PoolObjectManager>
{
    public int amountOfProjectiles;

    public Projectile bulletPrefab;
    public Projectile grenadPrefab;
    public Projectile tennisBallPrefab;

    private Dictionary<ProjectileType, List<Projectile>> pools;

    private void Awake()
    {
        pools = new Dictionary<ProjectileType, List<Projectile>>();

        var tennisBalls = new List<Projectile>();
        var bullets = new List<Projectile>();
        var grenades = new List<Projectile>();

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

    public GameObject GetPooledObject(ProjectileType projectileType)
    {
        Projectile obj = null;

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
                    obj = Instantiate(grenadPrefab, transform.position, Quaternion.identity);
                    break;
                case ProjectileType.TennisBall:
                    obj = Instantiate(tennisBallPrefab, transform.position, Quaternion.identity);
                    break;
                case ProjectileType.Bullet:
                    obj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    break;
            }
            obj.transform.SetParent(transform);
            obj.gameObject.SetActive(false);
        }

        return obj.gameObject;
    }

    public void ReturnToPool(Projectile objectToReturn)
    {
        objectToReturn.transform.SetParent(transform);
        pools[objectToReturn.projectileType].Add(objectToReturn);
    }
}
