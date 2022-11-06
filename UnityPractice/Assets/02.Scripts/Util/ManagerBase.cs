using UnityEngine;

public class ManagerBase<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Create(Transform parent)
    {
        T obj = new GameObject(typeof(T).ToString()).AddComponent<T>();
        obj.transform.SetParent(parent);
        obj.GetComponent<ManagerBase<T>>().Initialize();
        return obj;
    }

    protected virtual void Initialize() { }
}