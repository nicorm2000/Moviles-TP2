using System.Collections;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float minSpawnInterval;
    [SerializeField] private float maxSpawnInterval;
    [SerializeField] private float spawnXRange;
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
        yield return new WaitUntil(() => GameplayManager.hasStarted);

        while (true)
        {
            float randomSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);

            yield return new WaitForSeconds(randomSpawnInterval);
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