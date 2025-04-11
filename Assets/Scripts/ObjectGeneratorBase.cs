using System;
using System.Collections;
using UnityEngine;

public abstract class ObjectGeneratorBase : MonoBehaviour
{
    private WaitForEndOfFrame _wait = new WaitForEndOfFrame();

    public event Action ObjectDropped;
    public event Action ObjectInstantiated;
    public event Action ActiveObjectCountChanged;

    public int DroppedCount { get; protected set; }

    public abstract int InstantiateCount { get; }
    public abstract int ActiveCount { get; }

    protected void OnObjectDropped()
    {
        StartCoroutine(InvokeNextFrame(ObjectDropped));
        OnActiveObjectCountChanged();
    }

    protected void OnObjectInstantiated()
    {
        StartCoroutine(InvokeNextFrame(ObjectInstantiated));
    }

    protected void OnActiveObjectCountChanged()
    {
        StartCoroutine(InvokeNextFrame(ActiveObjectCountChanged));
    }

    private IEnumerator InvokeNextFrame(Action action)
    {
        yield return _wait;

        action?.Invoke();
    }
}
