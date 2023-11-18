using UnityEngine;

public class Coin : MonoBehaviour
{
    private void Update()
    {
        if (transform.position.y < CoinManager.Instance.despawnYPosition)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CollectCoin();
        }
    }

    private void CollectCoin()
    {
        int currentCoins = PlayerPrefs.GetInt("Coins", 0);
        currentCoins++;
        PlayerPrefs.SetInt("Coins", currentCoins);

        Debug.Log("Player's current coins: " + currentCoins);

        gameObject.SetActive(false);
    }
}