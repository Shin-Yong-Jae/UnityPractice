using UnityEngine;
using System.Collections.Generic;


[DisallowMultipleComponent]
[AddComponentMenu("")]
public class PoolObject : MonoBehaviour
{
    public string poolName;
    public bool isPooled;
}

public enum PoolInflationType
{
    INCREMENT,
    DOUBLE
}

class Pool
{
    private Stack<PoolObject> availableObjStack = new Stack<PoolObject>();

    private GameObject rootObj;
    private PoolInflationType inflationType;
    private string poolName;
    private int objectsInUse = 0;

    public Pool(string poolName, GameObject poolObjectPrefab, GameObject rootPoolObj, int initialCount, PoolInflationType type)
    {
        if (poolObjectPrefab == null)
        {
#if UNITY_EDITOR
            Debug.LogError("[ObjPoolManager] null pool object prefab !");
#endif
            return;
        }
        this.poolName = poolName;
        this.inflationType = type;
        this.rootObj = new GameObject(poolName + "Pool");
        this.rootObj.transform.SetParent(rootPoolObj.transform, false);

        GameObject go = GameObject.Instantiate(poolObjectPrefab);
        PoolObject po = go.GetComponent<PoolObject>();
        if (po == null)
        {
            po = go.AddComponent<PoolObject>();
        }
        po.poolName = poolName;
        AddObjectToPool(po);

        populatePool(Mathf.Max(initialCount, 1));
    }

    public void DeletePool()
    {
        if (rootObj != null)
            GameObject.Destroy(rootObj);
        poolName = null;
    }

    private void AddObjectToPool(PoolObject po)
    {
        po.gameObject.SetActive(false);
        po.gameObject.name = poolName;
        availableObjStack.Push(po);
        po.isPooled = true;
        po.gameObject.transform.SetParent(rootObj.transform, false);
    }

    private void populatePool(int initialCount)
    {
        for (int index = 0; index < initialCount; index++)
        {
            PoolObject po = GameObject.Instantiate(availableObjStack.Peek());
            AddObjectToPool(po);
        }
    }

    public GameObject NextAvailableObject(bool autoActive)
    {
        PoolObject po = null;
        if (availableObjStack.Count > 1)
        {
            po = availableObjStack.Pop();
        }
        else
        {
            int increaseSize = 0;
            if (inflationType == PoolInflationType.INCREMENT)
            {
                increaseSize = 1;
            }
            else if (inflationType == PoolInflationType.DOUBLE)
            {
                increaseSize = availableObjStack.Count + Mathf.Max(objectsInUse, 0);
            }
#if UNITY_EDITOR
            Debug.Log(string.Format("Growing pool {0}: {1} populated", poolName, increaseSize));
#endif
            if (increaseSize > 0)
            {
                populatePool(increaseSize);
                po = availableObjStack.Pop();
            }
        }

        GameObject result = null;
        if (po != null)
        {
            objectsInUse++;
            po.isPooled = false;
            result = po.gameObject;
            if (autoActive)
            {
                result.SetActive(true);
            }
        }

        return result;
    }

    public void ReturnObjectToPool(PoolObject po)
    {
        if (poolName.Equals(po.poolName))
        {
            objectsInUse--;

            if (po.isPooled)
            {
#if UNITY_EDITOR
                Debug.LogWarning(po.gameObject.name + " is already in pool. Why are you trying to return it again? Check usage.");
#endif
            }
            else
            {
                AddObjectToPool(po);
            }
        }
        else
        {
            Debug.LogError(string.Format("Trying to add object to incorrect pool {0} {1}", po.poolName, poolName));
        }
    }
}
