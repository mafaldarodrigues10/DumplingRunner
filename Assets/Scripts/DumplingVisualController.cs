using UnityEngine;

public class DumplingVisualController : MonoBehaviour
{
    public GameObject idle;
    public GameObject runLeft;
    public GameObject runRight;
    public GameObject jump;
    public GameObject slide;
    public GameObject ball;

    void SetActiveOnly(GameObject obj)
    {
        idle.SetActive(false);
        runLeft.SetActive(false);
        runRight.SetActive(false);
        jump.SetActive(false);
        slide.SetActive(false);
        ball.SetActive(false);

        obj.SetActive(true);
    }

    public void RunLeft() => SetActiveOnly(runLeft);
    public void RunRight() => SetActiveOnly(runRight);
    public void Jump() => SetActiveOnly(jump);
    public void Slide(bool active)
    {
        if (active) SetActiveOnly(slide);
        else SetActiveOnly(runLeft);
    }
    public void Caught() => SetActiveOnly(ball);
}
