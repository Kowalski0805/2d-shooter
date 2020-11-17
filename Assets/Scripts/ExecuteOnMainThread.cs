using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;

public class ExecuteOnMainThread : MonoBehaviour
{

    public readonly ConcurrentQueue<Action> RunOnMainThread = new ConcurrentQueue<Action>();

    void Start() {
        DontDestroyOnLoad(this);
    }
    
    void Update()
    {
        if (!RunOnMainThread.IsEmpty)
        {
            Action action;
            while (RunOnMainThread.TryDequeue(out action))
            {
                action.Invoke();
            }
        }
    }

}