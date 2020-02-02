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
        Vector3 spawnLocation = Quaternion.AngleAxis(Random.Range(0, 180), new Vector3(0,0,1)) * new Vector3(0, spawnLocationDelta, 0);
        spawnLocation = spawnLocation + transform.position;

        GameObject objectToSpawn = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];
        Instantiate(objectToSpawn, spawnLocation, Quaternion.identity);

        spawnTimer = Random.Range(-spawnRateDelta, spawnRateDelta);
    }
}
