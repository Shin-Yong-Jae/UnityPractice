using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskQueueManager<T> : ManagerBase<T> where T : MonoBehaviour
{
    private List<Action> actions = new List<Action>();

    protected void AddAction(Action action)
    {
        actions.Add(action);
    }

    protected void RemoveAction(Action action)
    {
        actions.Remove(action);
    }

    public void Next()
    {
        if (actions.Count == 0)
            return;

        Action action = actions[0];
        actions.RemoveAt(0);
        action.Invoke();
    }

    protected void Clear()
    {
        actions.Clear();
    }
}
