using UnityEngine;

public class Exploder
{
    private const int MaxExplodeObjects = 25;

    private float _explosionRadius = 20f;
    private float _explosionForce = 300f;
    private Collider[] _hitColliders = new Collider[MaxExplodeObjects];

    public Exploder(float explosionRadius, float explosionForce)
    {
        _explosionRadius = explosionRadius;
        _explosionForce = explosionForce;
    }

    public void Explode(Vector3 position)
    {
        int hits = Physics.OverlapSphereNonAlloc(position, _explosionRadius, _hitColliders);

        for (int i = 0; i < hits; i++)
        {
            if (_hitColliders[i].TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(_explosionForce, position, _explosionRadius);
            }
        }
    }
}
