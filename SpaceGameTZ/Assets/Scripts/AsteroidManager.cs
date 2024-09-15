using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    [SerializeField] private GameObject asteroidPrefab;

    [SerializeField] private int numberOfAsteroidsOnAxisX;
    [SerializeField] private int numberOfAsteroidsOnAxisY;
    [SerializeField] private int numberOfAsteroidsOnAxisZ;
    [SerializeField] private int gridSpacing;

    [SerializeField] private GameObject spawnAroundObject;

    [SerializeField] private float rotationSpeed; 

    private List<GameObject> spawnedAsteroids = new List<GameObject>();

    private void Start()
    {
        if (spawnAroundObject != null)
        {
            float halfX = (numberOfAsteroidsOnAxisX - 1) * gridSpacing / 2f;
            float halfY = (numberOfAsteroidsOnAxisY - 1) * gridSpacing / 2f;
            float halfZ = (numberOfAsteroidsOnAxisZ - 1) * gridSpacing / 2f;

            for (int i = 0; i < numberOfAsteroidsOnAxisX; i++)
            {
                for (int j = 0; j < numberOfAsteroidsOnAxisY; j++)
                {
                    for (int k = 0; k < numberOfAsteroidsOnAxisZ; k++)
                    {
                        InstantiateAsteroids(i, j, k, halfX, halfY, halfZ);
                    }
                }
            }
        }
    }

    private void Update()
    {
        RotateAsteroids();
    }

    private void InstantiateAsteroids(int x, int y, int z, float halfX, float halfY, float halfZ)
    {
        float offsetX = Random.Range(-gridSpacing / 3f, gridSpacing / 3f);
        float offsetY = Random.Range(-gridSpacing / 3f, gridSpacing / 3f);
        float offsetZ = Random.Range(-gridSpacing / 3f, gridSpacing / 3f);

        Vector3 spawnPosition = new Vector3(
            spawnAroundObject.transform.position.x + (x * gridSpacing - halfX) + offsetX,
            spawnAroundObject.transform.position.y + (y * gridSpacing - halfY) + offsetY,
            spawnAroundObject.transform.position.z + (z * gridSpacing - halfZ) + offsetZ
        );

        GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity, transform);
        spawnedAsteroids.Add(asteroid); 
    }

    private void RotateAsteroids()
    {
        if (spawnAroundObject == null)
            return;

        foreach (GameObject asteroid in spawnedAsteroids)
        {
            asteroid.transform.RotateAround(spawnAroundObject.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}
