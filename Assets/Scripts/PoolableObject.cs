using UnityEngine;

public abstract class PoolableObject : MonoBehaviour
{
    public event System.Action<PoolableObject> Deactivated;

    public abstract void ResetState();

    protected void OnDeactivated()
    {
        Deactivated?.Invoke(this);
    }
}
