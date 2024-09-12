using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler pooler;

    public GameObject[] objects;
    public int[] amountToBuffer;
    public int defaultBufferAmount = 10;
    public List<GameObject>[] pooledObjects;
    public bool canGrow = true;

	[HideInInspector] public GameObject container;

    void Awake()
    {
        if (pooler == null)
        {
            pooler = this;
        }
        else
        {
            Debug.Log("Deleted Object Pooler");
            Destroy(this);
        }
		
		container = new GameObject("ObjectPool");
        pooledObjects = new List<GameObject>[objects.Length];

        int index = 0;

        foreach (GameObject theObject in objects)
        {
            pooledObjects[index] = new List<GameObject>();
            int bufferAmount;

            if (amountToBuffer[index] > 0)
            {
                bufferAmount = amountToBuffer[index];
            }
            else
            {
                bufferAmount = defaultBufferAmount;
            }

            for (int i = 0; i < bufferAmount; i++)
            {
                GameObject obj = (GameObject)Instantiate(theObject);
                obj.name = theObject.name;
                PoolObject(obj);
            }
            index++;
        }
    }

    public GameObject GetObject(GameObject objectType)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i].name == objectType.name)
            {
                for (int j = 0; j < pooledObjects[i].Count; j++)
                {
                    if (!pooledObjects[i][j].activeInHierarchy)
                    {
                        return pooledObjects[i][j];
                    }
                }
                if (canGrow)
                {
                    GameObject obj = (GameObject)Instantiate(objects[i]);
                    obj.name = objects[i].name;
                    PoolObject(obj);
					print ("Grow -> "+obj.name);
                    return obj;
                }
                break;
            }
        }

        return null;
    }

    public void PoolObject(GameObject objectToPool)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i].name == objectToPool.name)
            {
                objectToPool.SetActive(false);
                objectToPool.transform.parent = container.transform;
                pooledObjects[i].Add(objectToPool);
                return;
            }
        }
    }

    public GameObject[] GetAllObjects(GameObject objectType) {
        GameObject[] go;

        for (int i = 0; i < objects.Length; i++) {
            if (objects[i].name == objectType.name) {
                go = new GameObject[pooledObjects[i].Count];

                for (int j = 0; j < pooledObjects[i].Count; j++) {
                    go[j] = pooledObjects[i][j];
                }
                return go;
            }
        }

        return null;
    }

    public GameObject SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject obj = GetObject(prefab);
        if (obj == null) return null;
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);
        return obj;
    }
}
