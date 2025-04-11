using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ColorChanger))]
public class Bomb : PoolableObject
{
    private const int MaxExplodeObjects = 25;

    [SerializeField] private ColorChanger _colorChanger;
    [SerializeField] private float _explosionRadius = 20f;
    [SerializeField] private float _explosionForce = 300f;
    [SerializeField] private float _minLifeDuration = 2f;
    [SerializeField] private float _maxLifeDuration = 5f;

    private float _time;
    private WaitForEndOfFrame _wait;
    private Collider[] hitColliders = new Collider[MaxExplodeObjects];

    public override void ResetState()
    {
        _colorChanger.ResetColor();
    }

    public void Arm()
    {
        float lifeDuration = Random.Range(_minLifeDuration, _maxLifeDuration);
        StartCoroutine(Explode(lifeDuration));
    }

    private IEnumerator Explode(float delay)
    {
        _time = 0f;

        while (_time < delay)
        {
            _time += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, _time / delay);
            _colorChanger.SetColorAlpha(alpha);

            yield return _wait;
        }

        Explode();
    }

    private void Explode()
    {
        int hits = Physics.OverlapSphereNonAlloc(transform.position, _explosionRadius, hitColliders);

        for (int i = 0; i < hits; i++)
        {
            if (hitColliders[i].TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }

        OnDeactivated();
    }
}
