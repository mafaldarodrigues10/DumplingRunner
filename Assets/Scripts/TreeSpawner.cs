using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public GameObject treePrefab;
    public GameObject barracaPrefab;
    public GameObject lightPropPrefab;

    public Transform player;

    public float spawnZ = 0f;
    public float spacing = 10f;
    public float spawnAheadDistance = 50f;

    public float leftX = -6f;
    public float rightX = 6f;

    public float treeY = 0.8f;
    public float barracaY = 0.8f;
    public float lightY = 0f;

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            SpawnProps();
        }
    }

    void Update()
    {
        if (player == null) return;

        if (player.position.z + spawnAheadDistance > spawnZ)
        {
            SpawnProps();
        }
    }

    void SpawnProps()
    {
        int scenario = PlayerPrefs.GetInt("SelectedScenario", 0);
        // 0 = Day, 1 = Sunset, 2 = Night

        // árvores aparecem em todos
        SpawnTreePair();

        if (scenario == 1)
        {
            // Sunset: barracas
            SpawnBarracaPair();
        }

        if (scenario == 2)
        {
            // Night: luzes
            SpawnLightPair();
        }

        spawnZ += spacing;
    }

    void SpawnTreePair()
    {
        if (treePrefab == null) return;

        Instantiate(treePrefab, new Vector3(leftX, treeY, spawnZ), Quaternion.Euler(-90f, 0f, 0f));
        Instantiate(treePrefab, new Vector3(rightX, treeY, spawnZ + 5f), Quaternion.Euler(-90f, 0f, 0f));
    }

    void SpawnBarracaPair()
    {
        if (barracaPrefab == null) return;

        Instantiate(barracaPrefab, new Vector3(leftX, barracaY, spawnZ + 3f), Quaternion.identity);
        Instantiate(barracaPrefab, new Vector3(rightX, barracaY, spawnZ + 7f), Quaternion.identity);
    }

    void SpawnLightPair()
    {
        if (lightPropPrefab == null) return;

        Instantiate(lightPropPrefab, new Vector3(leftX, lightY, spawnZ + 3f), Quaternion.identity);
        Instantiate(lightPropPrefab, new Vector3(rightX, lightY, spawnZ + 7f), Quaternion.identity);
    }
}