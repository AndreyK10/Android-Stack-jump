using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private Platform platform;
    [SerializeField] private SpawnPoints[] spawnPoints;

    private void Start()
    {
        StartCoroutine(StartSpawning());
    }


    private IEnumerator StartSpawning()
    {
        while (true)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform;
            Instantiate(platform, spawnPoint.position, Quaternion.identity, spawnPoint);
            foreach (SpawnPoints point in spawnPoints)
            {
                point.transform.position = new Vector3(point.transform.position.x, point.transform.position.y + 1f, point.transform.position.z);
            }
            yield return new WaitForSeconds(3f);
        }
    }


}
