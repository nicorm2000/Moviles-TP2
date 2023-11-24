using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float minFallSpeed;
    [SerializeField] private float maxFallSpeed;

    private float fallSpeed;

    private void Start()
    {
        fallSpeed = Random.Range(minFallSpeed, maxFallSpeed);
    }

    private void Update()
    {
        if (GameplayManager.hasStarted)
        {
            transform.position += Vector3.down * Time.deltaTime * fallSpeed;

            if (transform.position.y < CoinManager.Instance.despawnYPosition)
            {
                Debug.Log("Coin despawned");
                gameObject.SetActive(false);
            }
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
        Debug.Log("Player collected coin");
        PlayerPrefs.SetInt("Coins", currentCoins);
        Debug.Log("Player's current coins: " + currentCoins);
        gameObject.SetActive(false);
    }
}