using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CubeGenerator : MonoBehaviour
{
	[SerializeField] private CubeCollisionDetector cubePrefab;
	[SerializeField] private float cubeRate = 2f;
	[SerializeField] private Vector3 createPosition;
	[SerializeField] private float maxHorizontalOffset = 4f;
	[SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 10;

    private ObjectPool<CubeCollisionDetector> _pool;

	private void Awake()
	{
		_pool = new ObjectPool<CubeCollisionDetector>(
			createFunc: () => CreatePolledCube(),
			actionOnGet: (cube) => SetCubeAtRainPis(cube),
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

	private CubeCollisionDetector CreatePolledCube()
	{
		CubeCollisionDetector cube = Instantiate(cubePrefab);
        cube.gameObject.SetActive(false);
		cube.InitSettings(DeactivateCube);

        return cube;
    }

    private void SetCubeAtRainPis(CubeCollisionDetector cube)
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
		WaitForSeconds wait = new WaitForSeconds(cubeRate);

		while (enabled)
		{
			yield return wait;
			_pool.Get();
        }
	}

	private void DeactivateCube(CubeCollisionDetector cube)
	{
		_pool.Release(cube);
    }
}
