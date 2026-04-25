using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int value = 10;

    private void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("Player"))
        {
            ScoreManager.instance.AddCoin(value);
            if (AudioManager.instance != null)
            AudioManager.instance.PlayCoin();
            Destroy(gameObject);
        }
    }
}