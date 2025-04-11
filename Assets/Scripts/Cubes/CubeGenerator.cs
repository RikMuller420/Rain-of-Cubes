using System.Collections;
using UnityEngine;

public class CubeGenerator : ObjectGenerator<Cube>
{
    [SerializeField, Min(0.1f)] private float _createRate = 0.8f;
    [SerializeField] private Vector3 _createPosition = Vector3.zero;
    [SerializeField, Min(0)] private float _maxCreateHorizontalOffset = 4f;

    public event System.Action<Cube> CubeDropped;

    private void Start()
    {
        StartCoroutine(CreateCubesRepeating());
    }

    private IEnumerator CreateCubesRepeating()
    {
        WaitForSeconds wait = new WaitForSeconds(_createRate);

        while (enabled)
        {
            yield return wait;

            Pool.Get();
        }
    }

    protected override void DropObject(Cube cube)
    {
        base.DropObject(cube);

        Vector3 randomOffset = new Vector3(
            Random.Range(-_maxCreateHorizontalOffset, _maxCreateHorizontalOffset),
            0,
            Random.Range(-_maxCreateHorizontalOffset, _maxCreateHorizontalOffset)
        );
        cube.transform.position = _createPosition + randomOffset;

        CubeDropped?.Invoke(cube);
    }
}
