using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSubscriber : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.OnGameEnd += OnGameEnd;
        EventManager.OnGameClear += OnGameClear;
        EventManager.OnRestart += OnRestart;
        EventManager.OnDataSet += OnDataSet;
    }
    private void OnDisable()
    {
        EventManager.OnGameEnd -= OnGameEnd;
        EventManager.OnGameClear -= OnGameClear;
        EventManager.OnRestart -= OnRestart;
        EventManager.OnDataSet -= OnDataSet;
    }

    public virtual void OnGameEnd() { }
    public virtual void OnGameClear() { }
    public virtual void OnRestart() { }
    public virtual void OnDataSet() { }
}
