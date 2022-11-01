using UnityEngine;
using System.Collections;

namespace UnityEngine.UI
{
    [System.Serializable]
    public class LoopScrollPrefabSource
    {
        public string prefabName;
        public int poolSize = 5;

        private bool inited = false;
        public virtual GameObject GetObject()
        {
            if (!inited)
            {
                ResourceManager.Instance.InitPool(prefabName, poolSize);
                inited = true;
            }
            return ResourceManager.Instance.GetObjectFromPool(prefabName);
        }

        public virtual void ReturnObject(Transform go)
        {
            go.SendMessage("ScrollCellReturn", SendMessageOptions.DontRequireReceiver);
            ResourceManager.Instance.ReturnObjectToPool(go.gameObject);
        }
    }


    [System.Serializable]
    public class LoopScrollPrefabSourceLink
    {
        public GameObject prefab;
        public int poolSize = 5;

        private bool inited = false;
        public virtual GameObject GetObject()
        {
            if (!inited)
            {
                ResourceManager.Instance.InitPool(prefab, poolSize);
                inited = true;
            }
            return ResourceManager.Instance.GetObjectFromPool(prefab);
        }

        public virtual void ReturnObject(Transform go)
        {
            go.SendMessage("ScrollCellReturn", SendMessageOptions.DontRequireReceiver);
            ResourceManager.Instance.ReturnObjectToPool(go.gameObject);
        }
    }
}
