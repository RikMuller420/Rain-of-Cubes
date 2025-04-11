using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ColorChanger))]
public class Cube : PoolableObject
{
    [SerializeField] private ColorChanger _colorChanger;
    [SerializeField] private float _minLifeDuration = 2f;
    [SerializeField] private float _maxLifeDuration = 5f;

    private bool _isFallen = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (_isFallen)
        {
            return;
        }

        _isFallen = true;
        _colorChanger.SetRandomColor();
        float lifeDuration = Random.Range(_minLifeDuration, _maxLifeDuration);
        StartCoroutine(DeactivateCube(lifeDuration));
    }

    public override void ResetState()
    {
        _colorChanger.ResetColor();
        _isFallen = false;
    }

    private IEnumerator DeactivateCube(float delay)
    {
        yield return new WaitForSeconds(delay);

        OnDeactivated();
    }
}
