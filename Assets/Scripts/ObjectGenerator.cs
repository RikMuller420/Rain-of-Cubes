using UnityEngine;
using UnityEngine.Pool;

public class ObjectGenerator<T> : ObjectGeneratorBase where T : PoolableObject
{
    protected ObjectPool<T> Pool;

    [SerializeField] private T _prefab;

    private int _poolCapacity = 20;
    private int _poolMaxSize = 20;

    public override int InstantiateCount => Pool.CountAll;
    public override int ActiveCount => Pool.CountActive;

    private void Awake()
	{
		Pool = new ObjectPool<T>(
			createFunc: () => InstantiatePrefab(),
			actionOnGet: (instance) => DropObject(instance),
			actionOnRelease: (instance) => instance.gameObject.SetActive(false),
			actionOnDestroy: (instance) => DestroyObject(instance),
			collectionCheck: true,
			defaultCapacity: _poolCapacity,
			maxSize: _poolMaxSize
        );
	}

    protected virtual void DropObject(T instance)
    {
        instance.ResetState();
        instance.gameObject.SetActive(true);
        DroppedCount++;
        OnObjectDropped();
    }

    private T InstantiatePrefab()
	{
		T instance = Instantiate(_prefab);
        instance.gameObject.SetActive(false);
        instance.Deactivated += ReleaseInPool;
        OnObjectInstantiated();

        return instance;
    }

    private void ReleaseInPool(PoolableObject instance)
    {
        Pool.Release(instance as T);
        OnActiveObjectCountChanged();
    }

    private void DestroyObject(PoolableObject instance)
    {
        instance.Deactivated -= ReleaseInPool;
        Destroy(instance);
        OnActiveObjectCountChanged();
    }
}
