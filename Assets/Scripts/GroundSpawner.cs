using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundPrefab;
    public GameObject treeChunkPrefab;    
    public Transform player;
    public float spawnZ = 0f;
    public float groundLength = 50f;

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            SpawnGround();
        }
    }

    void Update()
    {
        if (player.position.z + 100 > spawnZ)
        {
            SpawnGround();
        }
    }

    void SpawnGround()
    {
        Instantiate(groundPrefab, new Vector3(0, 0, spawnZ), Quaternion.identity);
        spawnZ += groundLength;

        if (treeChunkPrefab != null)
        {
            Instantiate(treeChunkPrefab, new Vector3(0, 0, spawnZ), Quaternion.identity);
        }
    }
}