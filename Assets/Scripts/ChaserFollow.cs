using UnityEngine;

public class ChaserFollow : MonoBehaviour
{
    public Transform player;
    public Transform holdPoint;

    public float startDistance = 6f;
    public float catchSpeed = 12f;
    public float stopDistance = 1.2f;

    private bool isCatching = false;
    private bool hasCaught = false;

    void Start()
    {
        SetVisible(false);
    }

    void Update()
    {
        if (!isCatching || hasCaught || player == null) return;

        Vector3 targetPos = player.position - Vector3.forward * stopDistance;
        targetPos.y = transform.position.y;
        targetPos.x = player.position.x;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPos,
            catchSpeed * Time.deltaTime
        );

        float distance = Vector3.Distance(transform.position, targetPos);

        if (distance <= 0.05f)
        {
            CatchPlayer();
        }
    }

    public void StartCatch()
    {
        if (player == null) return;

        Vector3 startPos = player.position - Vector3.forward * startDistance;
        startPos.y = transform.position.y;
        startPos.x = player.position.x;

        transform.position = startPos;

        SetVisible(true);
        isCatching = true;
        hasCaught = false;
    }

    void CatchPlayer()
{
    hasCaught = true;
    isCatching = false;

    OldLadyVisualController oldLadyVisual = GetComponentInChildren<OldLadyVisualController>();

    if (oldLadyVisual != null)
        oldLadyVisual.GrabPlayer();

    // esconder o dumpling
    DumplingVisualController dumplingVisual = player.GetComponent<PlayerMovement>().visual;

    if (dumplingVisual != null)
    {
        dumplingVisual.gameObject.SetActive(false);
    }

    // opcional: parar física do player
    Rigidbody playerRb = player.GetComponent<Rigidbody>();

    if (playerRb != null)
    {
        playerRb.linearVelocity = Vector3.zero;
        playerRb.angularVelocity = Vector3.zero;
    }
}

    void SetVisible(bool visible)
    {
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.enabled = visible;
        }
    }
}