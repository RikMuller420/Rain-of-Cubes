using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ColorChanger))]
public class FallingObject : MonoBehaviour
{
    [SerializeField] private ColorChanger _colorChanger;

    private bool _isFallen = false;
    private float _minLifeDuration = 2f;
    private float _maxLifeDuration = 5f;
    private System.Action _deactivateDelegate;

    public void SetDeactivateAction(System.Action deactivateDelegate)
    {
        _deactivateDelegate = deactivateDelegate;
    }

    public void ResetState()
    {
        _colorChanger.ResetColor();
        _isFallen = false;
    }

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

    private IEnumerator DeactivateCube(float delay)
    {
        yield return new WaitForSeconds(delay);

        _deactivateDelegate?.Invoke();
    }
}
