using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement movement = GetComponent<PlayerMovement>();

        if (other.gameObject.name.Contains("Obstacle"))
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.GameOver();
            }

            return;
        }
    }
}