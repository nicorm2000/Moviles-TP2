using System.Collections.Generic;
using UnityEngine;

public class CoinPool : MonoBehaviour
{
    public static CoinPool Instance;

    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private int poolSize = 10;

    private List<GameObject> coinPool;

    private void Awake()
    {
        Instance = this;
        InitializePool();
    }

    private void InitializePool()
    {
        coinPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject coin = Instantiate(coinPrefab);
            coin.SetActive(false);
            coinPool.Add(coin);
        }
    }

    public GameObject GetCoin()
    {
        foreach (GameObject coin in coinPool)
        {
            if (!coin.activeInHierarchy)
            {
                return coin;
            }
        }

        GameObject newCoin = Instantiate(coinPrefab);
        coinPool.Add(newCoin);
        return newCoin;
    }
}