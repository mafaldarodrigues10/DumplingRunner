using UnityEngine;

public class OldLadyVisualController : MonoBehaviour
{
    public GameObject runLeft;
    public GameObject runRight;
    public GameObject grab;
    public GameObject hold;

    private float timer;
    private bool left = true;
    private bool isGrabbing = false;

    void Start()
    {
        ShowRunLeft();
    }

    void Update()
    {
        if (isGrabbing) return;

        timer += Time.deltaTime;

        if (timer > 0.25f)
        {
            timer = 0f;
            left = !left;

            if (left) ShowRunLeft();
            else ShowRunRight();
        }
    }

    public void GrabPlayer()
    {
        isGrabbing = true;

        if (runLeft != null) runLeft.SetActive(false);
        if (runRight != null) runRight.SetActive(false);
        if (grab != null) grab.SetActive(true);
        if (hold != null) hold.SetActive(false);
    }

    public void HoldPlayer()
    {
        isGrabbing = true;

        if (runLeft != null) runLeft.SetActive(false);
        if (runRight != null) runRight.SetActive(false);
        if (grab != null) grab.SetActive(false);
        if (hold != null) hold.SetActive(true);
    }

    void ShowRunLeft()
    {
        if (runLeft != null) runLeft.SetActive(true);
        if (runRight != null) runRight.SetActive(false);
        if (grab != null) grab.SetActive(false);
        if (hold != null) hold.SetActive(false);
    }

    void ShowRunRight()
    {
        if (runLeft != null) runLeft.SetActive(false);
        if (runRight != null) runRight.SetActive(true);
        if (grab != null) grab.SetActive(false);
        if (hold != null) hold.SetActive(false);
    }
}