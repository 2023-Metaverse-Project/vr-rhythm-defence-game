using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private float factor = 1;
    [SerializeField]
    private float delayPerSpawnGroup = 3;

    public void GameOver()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;

            child.GetComponent<Enemy>().Victory();
        }
    }

    public void Play()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            GameObject.Destroy(child);
        }
        StartCoroutine(SpawnProcess());
    }

    public void Stop()
    {
        StopAllCoroutines();
        GameOver();
    }

    public void Restart()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            GameObject.Destroy(child);
        }

        StartCoroutine(SpawnProcess());
    }

    private IEnumerator SpawnProcess()
    {
        float factor = this.factor;

        while (true)
        {
            yield return new WaitForSeconds(delayPerSpawnGroup);

            yield return StartCoroutine(SpawnEnemy(factor));

            delayPerSpawnGroup = Random.Range(5, 10);
            this.factor = Random.Range(1, 5);
        }
    }

    private IEnumerator SpawnEnemy(float factor)
    {
        float spawnCount = factor;

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
