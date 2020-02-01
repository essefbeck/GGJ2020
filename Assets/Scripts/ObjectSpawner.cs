using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn;

    public float spawnRate;

    public float spawnRateDelta;

    public float spawnLocationDelta;

    private float spawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnRate)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        Vector3 location = transform.position + new Vector3(Random.Range(-spawnLocationDelta, spawnLocationDelta), Random.Range(-spawnLocationDelta, spawnLocationDelta), 0);
        GameObject objectToSpawn = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];
        Instantiate(objectToSpawn, location, Quaternion.identity);

        spawnTimer = Random.Range(-spawnRateDelta, spawnRateDelta);
    }
}
