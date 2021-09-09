using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private Platform platform;
    [SerializeField] private SpawnPoints[] spawnPoints;

    public List<Platform> spawnedPlatforms { get; private set; } = new List<Platform>();

    [SerializeField] private float spawnDelay;

    private void Start()
    {
        StartCoroutine(StartSpawning());
    }


    private IEnumerator StartSpawning()
    {
        while (enabled)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform;
            Platform newPlatform = Instantiate(platform, spawnPoint.position, Quaternion.identity, spawnPoint);
            spawnedPlatforms.Add(newPlatform);
            foreach (SpawnPoints point in spawnPoints)
            {
                point.transform.position = new Vector3(point.transform.position.x, point.transform.position.y + 1f, point.transform.position.z);
            }
            yield return new WaitForSeconds(spawnDelay);
        }
    }


}
