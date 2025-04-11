using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ColorChanger))]
public class Bomb : PoolableObject
{
    [SerializeField] private ColorChanger _colorChanger;
    [SerializeField] private float _explosionRadius = 20f;
    [SerializeField] private float _explosionForce = 300f;
    [SerializeField] private float _minLifeDuration = 2f;
    [SerializeField] private float _maxLifeDuration = 5f;

    private float _time;
    private WaitForEndOfFrame _wait;
    private Exploder _exploder;

    private void Awake()
    {
        _exploder = new Exploder(_explosionRadius, _explosionForce);
    }

    public override void ResetState()
    {
        _colorChanger.ResetColor();
    }

    public void StartExplodeTimer()
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

        _exploder.Explode(transform.position);
        OnDeactivated();
    }
}
