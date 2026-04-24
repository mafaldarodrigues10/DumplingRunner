using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Coin Settings")]
    public GameObject coinPrefab;
    public float groundCoinY = 0.8f;

    [Header("Obstacles")]
    public GameObject groundObstacle;
    public GameObject slideObstacle;
    public GameObject tallObstacle;
    public GameObject longTallObstacle;
    public GameObject rampTallObstacle;

    public Transform player;

    [Header("Spawn Settings")]
    public float spawnZ = 60f;
    public float rowDistance = 18f;
    public float laneDistance = 2.5f;
    public int initialRows = 16;
    public float spawnAheadDistance = 160f;

    private int lastPattern = -1;

    
    // 0 = Empty, 1 = Ground, 2 = Slide, 3 = Tall, 4 = LongTall, 5 = RampTall
    private int[,] patterns = new int[,]
    {
        {1, 1, 1}, // G G G
        {1, 0, 1}, // G _ G
        {3, 0, 3}, // T _ T
        {2, 0, 1}, // S _ G
        {1, 0, 2}, // G _ S
        {3, 2, 0}, // T S _
        {0, 2, 3}, // _ S T
        {1, 1, 0}, // G G _
        {0, 1, 1}, // _ G G
        {1, 2, 1}, // G S G
        {1, 3, 1}, // G T G
        {2, 1, 2}, // S G S
        {3, 1, 3}, // T G T
        {2, 3, 3}, // S T T
        {3, 3, 2}, // T T S
        {3, 3, 1}, // T T G
        {1, 3, 3}, // G T T

        {4, 0, 4}, // L _ L
        {1, 4, 1}, // G L G
        {4, 1, 0}, // L G _
        {0, 1, 4}, // _ G L
        {2, 4, 0}, // S L _
        {0, 4, 2}, // _ L S
        {0, 4, 4}, // _ L L
        {4, 4, 0}, // L L _

        {5, 0, 1}, // R _ G
        {1, 0, 5}, // G _ R
        {0, 5, 0}, // _ R _
        {5, 1, 0}, // R G _
        {0, 1, 5}, // _ G R
        {5, 5, 0}, // R R _
        {0, 5, 5}, // _ R R
        {5, 0, 5}  // R _ R
    };

    void Start()
    {
        for (int i = 0; i < initialRows; i++)
        {
            SpawnPattern();
        }
    }

    void Update()
    {
        if (player != null && player.position.z + spawnAheadDistance > spawnZ)
        {
            SpawnPattern();
        }
    }

    void SpawnPattern()
    {
        int patternIndex = Random.Range(0, patterns.GetLength(0));

        while (patternIndex == lastPattern)
        {
            patternIndex = Random.Range(0, patterns.GetLength(0));
        }

        lastPattern = patternIndex;

        int[] rowContents = new int[3];

        for (int lane = 0; lane < 3; lane++)
        {
            int content = patterns[patternIndex, lane];
            rowContents[lane] = content;
            SpawnLaneContent(lane, content);
        }

        SpawnCoinsForRow(rowContents);

        spawnZ += rowDistance;
    }

    void SpawnLaneContent(int lane, int content)
    {
        switch (content)
        {
            case 0:
                return;
            case 1:
                SpawnPrefabInLane(groundObstacle, lane, spawnZ);
                break;
            case 2:
                SpawnPrefabInLane(slideObstacle, lane, spawnZ);
                break;
            case 3:
                SpawnPrefabInLane(tallObstacle, lane, spawnZ);
                break;
            case 4:
                SpawnPrefabInLane(longTallObstacle, lane, spawnZ);
                break;
            case 5:
                SpawnPrefabInLane(rampTallObstacle, lane, spawnZ);
                break;
        }
    }

    void SpawnPrefabInLane(GameObject obstaclePrefab, int lane, float zPos)
    {
        if (obstaclePrefab == null) return;

        float xPos = LaneToX(lane);

        Vector3 pos = obstaclePrefab.transform.position;
        pos.x = xPos;
        pos.z = zPos;

        Instantiate(obstaclePrefab, pos, Quaternion.identity);
    }

    float LaneToX(int lane)
    {
        return (lane - 1) * laneDistance;
    }

    void SpawnCoinsForRow(int[] rowContents)
    {
        if (coinPrefab == null) return;

        for (int lane = 0; lane < 3; lane++)
        {
            int content = rowContents[lane];

            // Espaço em branco
            if (content == 0)
            {
                SpawnSingleCoin(lane, spawnZ);
            }

            // Antes e depois de Ground, Slide e Ramp
            if (content == 1 || content == 2 || content == 5)
            {
                if (Random.value < 0.5f) SpawnSingleCoin(lane, spawnZ - 4f);
                if (Random.value < 0.5f) SpawnSingleCoin(lane, spawnZ + 4f);
            }
        }
    }

    void SpawnSingleCoin(int lane, float zPos)
    {
        float xPos = LaneToX(lane);
        Vector3 pos = new Vector3(xPos, groundCoinY, zPos);

        Instantiate(coinPrefab, pos, Quaternion.Euler(90f, 0f, 0f));
    }

}