using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CubeGenerator : MonoBehaviour
{
	[SerializeField] private FallingObject cubePrefab;
	[SerializeField] private float createRate = 0.8f;
	[SerializeField] private Vector3 createPosition = Vector3.zero;
	[SerializeField] private float maxHorizontalOffset = 4f;
	[SerializeField] private int _poolCapacity = 20;
    [SerializeField] private int _poolMaxSize = 20;

    private ObjectPool<FallingObject> _pool;

	private void Awake()
	{
		_pool = new ObjectPool<FallingObject>(
			createFunc: () => InstantiateCube(),
			actionOnGet: (cube) => DropNewCube(cube),
			actionOnRelease: (cube) => cube.gameObject.SetActive(false),
			actionOnDestroy: (cube) => Destroy(cube),
			collectionCheck: true,
			defaultCapacity: _poolCapacity,
			maxSize: _poolMaxSize
        );
	}

	private void Start()
	{
		StartCoroutine(ActivateCube());
	}

	private FallingObject InstantiateCube()
	{
		FallingObject cube = Instantiate(cubePrefab);
        cube.gameObject.SetActive(false);
		cube.SetDeactivateAction(ReleaseCube);

        return cube;
    }

    private void DropNewCube(FallingObject cube)
	{
		Vector3 randomOffset = new Vector3(
			Random.Range(-maxHorizontalOffset, maxHorizontalOffset),
			0,
            Random.Range(-maxHorizontalOffset, maxHorizontalOffset)
        );

        cube.transform.position = createPosition + randomOffset;
        cube.ResetState();
        cube.gameObject.SetActive(true);
    }

    private IEnumerator ActivateCube()
	{
		WaitForSeconds wait = new WaitForSeconds(createRate);

		while (enabled)
		{
			yield return wait;
			_pool.Get();
        }
	}

	private void ReleaseCube(FallingObject cube)
	{
		_pool.Release(cube);
    }
}
