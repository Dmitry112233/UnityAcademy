using UnityEngine;

public class ResetEnemies : MonoBehaviour
{
    public GameObject[] enemiesPrefabs;
    public Transform[] spawnPositions;
 
    public void RespawnEnemies() 
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for(int i = 0; i < enemiesPrefabs.Length; i++) 
        {
            var enemy = Instantiate(enemiesPrefabs[i], spawnPositions[i].position, Quaternion.identity);
            enemy.transform.SetParent(transform);
        }
    }
}