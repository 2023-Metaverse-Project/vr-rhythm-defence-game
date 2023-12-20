using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private bool isPlayOnStart = true;
    [SerializeField]
    private float startFactor = 1;
    [SerializeField]
    private float additiveFactor = 0.1f;
    [SerializeField]
    private float delayPerSpawnGroup = 3;

    private void Awake()
    {
        if (isPlayOnStart)
        {
            Play();
        }
    }

    public void Play()
    {
        StartCoroutine(nameof(SpawnProcess));
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnProcess()
    {
        float factor = startFactor;

        while (true)
        {
            yield return new WaitForSeconds(delayPerSpawnGroup);

            yield return StartCoroutine(SpawnEnemy(factor));

            factor += additiveFactor;
        }
    }

    private IEnumerator SpawnEnemy(float factor)
    {
        float spawnCount = Random.Range(factor, factor * 2);

        for (int i = 0; i < spawnCount; ++i)
        {
            Instantiate(enemyPrefab, transform.position, transform.rotation, transform);

            if (Random.value < 0.2f)
            {
                yield return new WaitForSeconds(Random.Range(0.01f, 0.02f));
            }
        }
    }
}
