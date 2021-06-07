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
    }
    private void OnDisable()
    {
        EventManager.OnGameEnd -= OnGameEnd;
        EventManager.OnGameClear -= OnGameClear;
        EventManager.OnRestart -= OnRestart;
    }

    public virtual void OnGameEnd() { }
    public virtual void OnGameClear() { }
    public virtual void OnRestart() { }
}
