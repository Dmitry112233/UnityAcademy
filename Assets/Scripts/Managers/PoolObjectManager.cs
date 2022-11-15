using System.Collections.Generic;
using UnityEngine;

public class PoolObjectManager : Singleton<PoolObjectManager>
{
    public int amountOfProjectiles;

    public Projectile bulletPrefab;
    public Projectile grenadPrefab;
    public Projectile tennisBallPrefab;

    private List<Projectile> pool;

    private void Awake()
    {
        pool = new List<Projectile>();

        for(int i = 0; i < amountOfProjectiles; i++) 
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

    public GameObject GetPooledObject(Projectile objectToInstatiate) 
    {
        var obj = pool.Find(x => x.GetType() == objectToInstatiate.GetType());
        
        if (obj != null) 
        {
            pool.Remove(obj);
        }
        else 
        {
            obj = Instantiate(objectToInstatiate, transform.position, Quaternion.identity);
            obj.transform.SetParent(transform);
            obj.gameObject.SetActive(false);
        }

        return obj.gameObject;
    }

    public void ReturnToPool(Projectile objectToReturn) 
    {
        if(pool.FindAll(x => x.GetType() == objectToReturn.GetType()).Count < amountOfProjectiles) 
        {
            objectToReturn.transform.SetParent(transform);
            pool.Add(objectToReturn);
        }
        else 
        {
            Destroy(objectToReturn.gameObject);
        }
    }
}
