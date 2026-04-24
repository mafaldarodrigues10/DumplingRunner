using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int value = 10;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Coin trigger com: " + other.gameObject.name);

        if (other.gameObject.name.Contains("Player"))
        {
            Debug.Log("Player apanhou moeda");

            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.AddCoin(value);
            }

            Destroy(gameObject);
        }
    }
}