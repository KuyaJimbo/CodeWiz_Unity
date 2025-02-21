using System.Collections;
using UnityEngine;

public class ObjectTimedSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // The prefab to spawn
    public Vector3 spawnPosition; // The position where the object will spawn
    public float minSpawnRate = 1f; // Minimum spawn interval
    public float maxSpawnRate = 3f; // Maximum spawn interval

    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            float spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
            yield return new WaitForSeconds(spawnRate);
            
            if (objectToSpawn != null)
            {
                Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            }
        }
    }
}
