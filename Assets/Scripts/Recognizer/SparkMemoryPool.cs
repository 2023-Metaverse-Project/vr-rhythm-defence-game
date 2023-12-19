using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkMemoryPool : MonoBehaviour
{
    [SerializeField]
    private GameObject sparkPrefab;
    private MemoryPool memoryPool;

    private void Awake()
    {
        memoryPool = new MemoryPool(sparkPrefab);
    }

    public void spawnSpark(Vector3 position)
    {
        GameObject spark = memoryPool.ActivatePoolItem();
        spark.transform.position = position;

        spark.transform.rotation = Quaternion.identity;
        spark.GetComponent<Spark>().Setup(memoryPool);
    }
}
