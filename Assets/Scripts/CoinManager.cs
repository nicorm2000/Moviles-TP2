using System.Collections;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float spawnXRange = 4f;
    public float despawnYPosition = -5f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(SpawnCoins());
    }

    private IEnumerator SpawnCoins()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnCoin();
        }
    }

    private void SpawnCoin()
    {
        GameObject coin = CoinPool.Instance.GetCoin();

        float randomX = Random.Range(-spawnXRange, spawnXRange);

        coin.transform.position = new Vector2(randomX, spawnPoint.position.y);
        coin.SetActive(true);
    }
}