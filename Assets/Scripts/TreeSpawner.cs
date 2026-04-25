using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public GameObject treePrefab;
    public GameObject barracaPrefab;
    public GameObject lightPropPrefab;

    public Transform player;

    public float spawnZ = 0f;
    public float spacing = 14f;
    public float spawnAheadDistance = 80f;

    public float leftX = -8f;
    public float rightX = 8f;

    public float treeY = 0.8f;
    public float barracaY = 0.8f;
    public float lightY = 0f;

    private int currentScenario = -1;

    void Start()
    {
        currentScenario = PlayerPrefs.GetInt("CurrentRunScenario", 0);
        ResetProps();
    }

    void Update()
    {
        if (player == null) return;

        int scenario = PlayerPrefs.GetInt("CurrentRunScenario", 0);

        // se mudar cenário → limpa e recria
        if (scenario != currentScenario)
        {
            currentScenario = scenario;
            ResetProps();
        }

        // spawn contínuo
        if (player.position.z + spawnAheadDistance > spawnZ)
        {
            SpawnProps();
        }
    }

    void ResetProps()
    {
        // apagar tudo o que já foi spawnado
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        spawnZ = player != null ? player.position.z + 30f : 0f;

        for (int i = 0; i < 8; i++)
        {
            SpawnProps();
        }
    }

    void SpawnProps()
    {
        if (currentScenario == 0)
        {
            SpawnTreePair();
        }
        else if (currentScenario == 1)
        {
            SpawnBarracaPair();
        }
        else if (currentScenario == 2)
        {
            SpawnLightPair();
        }

        spawnZ += spacing;
    }

    void SpawnTreePair()
    {
        if (treePrefab == null) return;

        Instantiate(treePrefab, new Vector3(leftX, treeY, spawnZ), Quaternion.Euler(-90f, 0, 0), transform);
        Instantiate(treePrefab, new Vector3(rightX, treeY, spawnZ + 7f), Quaternion.Euler(-90f, 0, 0), transform);
    }

    void SpawnBarracaPair()
    {
        if (barracaPrefab == null) return;

        Instantiate(barracaPrefab, new Vector3(leftX, barracaY, spawnZ), Quaternion.Euler(-90f, 0, 0), transform);
        Instantiate(barracaPrefab, new Vector3(rightX, barracaY, spawnZ + 7f), Quaternion.Euler(-90f, 0, 0), transform);
    }

    void SpawnLightPair()
    {
        if (lightPropPrefab == null) return;

        Instantiate(lightPropPrefab, new Vector3(leftX, lightY, spawnZ), Quaternion.Euler(-90f, 0, 0), transform);
        Instantiate(lightPropPrefab, new Vector3(rightX, lightY, spawnZ + 7f), Quaternion.Euler(-90f, 0, 0), transform);
    }
}