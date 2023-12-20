using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectInfo
{
    public GameObject goPrefab;
    public int count; // 개수
    public Transform tfPoolParent; // 어느 부모의 자식 위치로 생성할 지 위치
}

public class ObjectPool : MonoBehaviour
{
    [SerializeField] ObjectInfo[] objectinfo = null;
    //objectinfo[0] => leftNotePrefab 지정
    //objectinfo[1] => rightNotePrefab 지정

    public static ObjectPool instance;

    public Queue<GameObject> leftNoteQueue = new Queue<GameObject>();
    public Queue<GameObject> rightNoteQueue = new Queue<GameObject>();

    private void Start()
    {
        instance = this;
        leftNoteQueue = InsertQueue(objectinfo[0]); // leftNotePrefab
        rightNoteQueue = InsertQueue(objectinfo[1]); // rightNotePrefab
    }

    Queue<GameObject> InsertQueue(ObjectInfo p_objectInfo)
    {
        Queue<GameObject> t_queue = new Queue<GameObject>();
        for(int i = 0;i<p_objectInfo.count;i++)
        {
            GameObject t_clone = Instantiate(p_objectInfo.goPrefab, transform.position, Quaternion.identity, p_objectInfo.tfPoolParent);
            t_clone.SetActive(false);
            if (p_objectInfo.tfPoolParent != null)
                t_clone.transform.SetParent(p_objectInfo.tfPoolParent);
            else
                t_clone.transform.SetParent(this.transform);

            t_queue.Enqueue(t_clone);
        }

        return t_queue;
    }
}
