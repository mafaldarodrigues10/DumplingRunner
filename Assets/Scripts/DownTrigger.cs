using UnityEngine;

public class DownTrigger : MonoBehaviour
{
    public float groundY = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            Vector3 pos = other.transform.position;
            pos.y = groundY;
            other.transform.position = pos;
        }
    }
}