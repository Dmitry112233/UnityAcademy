using System.Collections;
using UnityEngine;

public class ResetEnemies : MonoBehaviour
{
    public GameObject[] enemiesPrefabs;
    public Transform[] spawnPositions;
    public PlayerController playerController;

    private void Start()
    {
        playerController.NotifyPlayerDie += CallRespawnEnemiesCoroutine;
    }

    private void CallRespawnEnemiesCoroutine()
    {
        StartCoroutine(RespawnEnemies());
    }

    public IEnumerator RespawnEnemies() 
    {
        yield return new WaitForSeconds(2);

        foreach (Transform child in transform)
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