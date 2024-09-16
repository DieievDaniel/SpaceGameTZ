using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [Header("Coin Settings")]
    [SerializeField] private GameObject coinPrefab; 
    [SerializeField] private int numberOfCoins = 100; 

    [Header("Spawn Settings")]
    [SerializeField] private Vector3 spawnAreaSize = new Vector3(100f, 0f, 100f); 
    [SerializeField] private Vector3 spawnAreaCenter = new Vector3(0f, 0f, 0f); 

    private void Start()
    {
        SpawnCoins();
    }

    private void SpawnCoins()
    {
        for (int i = 0; i < numberOfCoins; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            Instantiate(coinPrefab, randomPosition, Quaternion.identity);
        }
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(spawnAreaCenter.x - spawnAreaSize.x / 2, spawnAreaCenter.x + spawnAreaSize.x / 2);
        float y = Random.Range(spawnAreaCenter.y - spawnAreaSize.y / 2, spawnAreaCenter.y + spawnAreaSize.y / 2);
        float z = Random.Range(spawnAreaCenter.z - spawnAreaSize.z / 2, spawnAreaCenter.z + spawnAreaSize.z / 2);
        return new Vector3(x, y, z); 
    }
}
