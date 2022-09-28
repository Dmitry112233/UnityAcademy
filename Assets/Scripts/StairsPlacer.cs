using System.Collections.Generic;
using UnityEngine;

public class StairsPlacer : MonoBehaviour
{
    public Transform playerTransform;
    public StairsChunk[] stairsPrefabs;
    public StairsChunk firstChunk;
    public Transform parent;

    private List<StairsChunk> spawnedChunks = new List<StairsChunk>();
    private float chunkDistance;

    void Start()
    {
        spawnedChunks.Add(firstChunk);
        chunkDistance = spawnedChunks[spawnedChunks.Count - 1].endPosition.position.y - spawnedChunks[spawnedChunks.Count - 1].beginPosition.position.y;
    }

    void Update()
    {
        if(playerTransform.position.y > spawnedChunks[spawnedChunks.Count - 1].endPosition.position.y - 50) 
        {
            SpawnStairsChunk();
        }
    }

    private void SpawnStairsChunk() 
    {
        StairsChunk newChunk = Instantiate(stairsPrefabs[Random.Range(0, stairsPrefabs.Length)]);
        newChunk.transform.position = spawnedChunks[spawnedChunks.Count - 1].transform.position;
        newChunk.transform.SetParent(parent);
        newChunk.transform.position = newChunk.transform.position + new Vector3(0.0f, chunkDistance, 0.0f);
        spawnedChunks.Add(newChunk);

        if(spawnedChunks.Count >= 3) 
        {
            Destroy(spawnedChunks[0].gameObject);
            spawnedChunks.RemoveAt(0);
            parent.position = parent.position - new Vector3(0.0f, chunkDistance, 0.0f);
        }
    }
}
