using UnityEngine;

public class RampTrigger : MonoBehaviour
{
    public float upperY = 2.25f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            Vector3 pos = other.transform.position;
            pos.y = upperY;
            other.transform.position = pos;
        }
    }
}
